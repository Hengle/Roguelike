﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fundamental
{
	public static class ListPool<T>
	{
		private const int MaxCapacitySearchLength = 8;

		private static List<object> pool;

		static ListPool()
		{
			ListPool<T>.pool = new List<object>();
		}

		public static List<T> Get()
		{
			if (ListPool<T>.pool.Count > 0)
			{
				List<T> result = (List<T>)ListPool<T>.pool[ListPool<T>.pool.Count - 1];
				ListPool<T>.pool.RemoveAt(ListPool<T>.pool.Count - 1);
				return result;
			}
			return new List<T>();
		}

		public static List<T> Get(int capacity)
		{
			if (ListPool<T>.pool.Count > 0)
			{
				List<T> list = null;
				int num = 0;
				while (num < ListPool<T>.pool.Count && num < 8)
				{
					list = (List<T>)ListPool<T>.pool[ListPool<T>.pool.Count - 1 - num];
					if (list.Capacity >= capacity)
					{
						ListPool<T>.pool.RemoveAt(ListPool<T>.pool.Count - 1 - num);
						return list;
					}
					num++;
				}
				if (list == null)
				{
					list = new List<T>(capacity);
				}
				else
				{
					list.Capacity = capacity;
					ListPool<T>.pool[ListPool<T>.pool.Count - num] = ListPool<T>.pool[ListPool<T>.pool.Count - 1];
					ListPool<T>.pool.RemoveAt(ListPool<T>.pool.Count - 1);
				}
				return list;
			}
			return new List<T>(capacity);
		}

		public static void Warmup(int count, int size)
		{
			List<T>[] array = new List<T>[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = ListPool<T>.Get(size);
			}
			for (int j = 0; j < count; j++)
			{
				ListPool<T>.Release(array[j]);
			}
		}

		public static void Release(List<T> list)
		{
			if (list == null)
			{
				return;
			}

			list.Clear();
			ListPool<T>.pool.Add(list);
		}

		public static void Clear()
		{
			ListPool<T>.pool.Clear();
		}

		public static int GetSize()
		{
			return ListPool<T>.pool.Count;
		}
	}
}