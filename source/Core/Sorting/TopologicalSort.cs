using System;
using System.Linq;
using System.Collections.Generic;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Sorting
{
	/// <summary>
	/// Implementation from Umbraco.Core
	/// Modified to improve usability.
	/// </summary>
	public class TopologicalSort
	{
		private readonly int[] _vertices;
		private readonly int[,] _matrix;
		private readonly int[] _sortedArray;

		private int _numVerts;

		public TopologicalSort(int size)
		{
			_vertices = new int[size];
			_matrix = new int[size, size];
			_numVerts = 0;

			for (int i = 0; i < size; ++i)
			{
				for (int j = 0; j < size; ++j)
					_matrix[i, j] = 0;
			}

			_sortedArray = new int[size];
		}

		public int AddVertex(int vertex)
		{
			_vertices[_numVerts++] = vertex;

			return _numVerts - 1;
		}

		public void AddEdge(int start, int end)
		{
			_matrix[start, end] = 1;
		}

		public int[] Sort()
		{
			while (_numVerts > 0)
			{
				int delVert = NoSuccessors();

				if (delVert == -1)
					throw new Exception("Graph has cycles");

				_sortedArray[this._numVerts - 1] = this._vertices[delVert];

				DeleteVertex(delVert);
			}

			return _sortedArray;
		}

		private int NoSuccessors()
		{
			for (int i = 0; i < _numVerts; ++i)
			{
				bool flag = false;

				for (int j = 0; j < _numVerts; ++j)
				{
					if (_matrix[i, j] > 0)
					{
						flag = true;
						break;
					}
				}

				if (!flag)
					return i;
			}

			return -1;
		}

		private void DeleteVertex(int delVert)
		{
			if (delVert != _numVerts - 1)
			{
				for (int index = delVert; index < _numVerts - 1; ++index)
					_vertices[index] = _vertices[index + 1];

				for (int row = delVert; row < _numVerts - 1; ++row)
					MoveRowUp(row, _numVerts);

				for (int col = delVert; col < _numVerts - 1; ++col)
					MoveColLeft(col, _numVerts - 1);
			}

			--_numVerts;
		}

		private void MoveRowUp(int row, int length)
		{
			for (int index = 0; index < length; ++index)
				_matrix[row, index] = _matrix[row + 1, index];
		}

		private void MoveColLeft(int col, int length)
		{
			for (int i = 0; i < length; ++i)
				_matrix[i, col] = _matrix[i, col + 1];
		}

		public static IEnumerable<T> Sort<T>(IList<DependencyField<T>> fields)
			where T : class
		{
			int[] topologicalSortOrder = GetTopologicalSortOrder<T>(fields);
			var list = new List<T>();

			for (int i = 0; i < topologicalSortOrder.Length; ++i)
			{
				DependencyField<T> dependencyField = fields[topologicalSortOrder[i]];
				list.Add(dependencyField.Item.Value);
			}

			list.Reverse();

			return list;
		}

		private static int[] GetTopologicalSortOrder<T>(IList<DependencyField<T>> fields)
			where T : class
		{
			var topologicalSorter = new TopologicalSort(fields.Count());
			var dictionary = new Dictionary<string, int>();

			for (int vertex = 0; vertex < fields.Count(); ++vertex)
				dictionary[fields[vertex].Alias.ToLowerInvariant()] = topologicalSorter.AddVertex(vertex);

			for (int start = 0; start < fields.Count; ++start)
			{
				if (fields[start].DependsOn != null)
				{
					foreach (string dependency in fields[start].DependsOn)
					{
						if (!dictionary.ContainsKey(dependency.ToLowerInvariant()))
							throw new IndexOutOfRangeException(string.Format("The alias '{0}' has an invalid dependency. The dependency '{1}' does not exist in the list of aliases", fields[start], dependency));

						topologicalSorter.AddEdge(start, dictionary[dependency.ToLowerInvariant()]);
					}
				}
			}

			return topologicalSorter.Sort();
		}
	}
}