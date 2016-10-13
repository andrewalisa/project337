﻿using UnityEngine;
using System.Collections;
using System;
public class Heap<T> where T : IHeapItem<T> {
	private T[] items;
	int currentItemCount;

	public Heap(int maxHeapSize) {
		items = new T[maxHeapSize];
	}

	public void Add(T item) {
		item.HeapIndex = currentItemCount;
		items [currentItemCount] = item;
		SortUp (item);
		currentItemCount++;
	}
	public T Pop() {
		T firstItem = items [0];
		currentItemCount--;
		items [0] = items [currentItemCount];
		items [0].HeapIndex = 0;
		SortDown (items [0]);
		return firstItem;
	}

	public void UpdateItem(T item) {
		SortUp (item);
	}

	public int Count {
		get {
			return currentItemCount;
		}
	}

	public bool Contains(T item) {
		return Equals (items [item.HeapIndex], item);
	}

	void SortDown(T item) {
		while (true) {
			int leftIndex = item.HeapIndex * 2 + 1;
			int rightIndex = item.HeapIndex * 2 + 2;
			int swapIndex = 0;

			if (leftIndex < currentItemCount) {
				swapIndex = leftIndex;

				if (rightIndex < currentItemCount) {
					if (items [leftIndex].CompareTo (items [rightIndex]) < 0) {
						swapIndex = rightIndex;
					}
				}
				if (item.CompareTo (items [swapIndex]) < 0) {
					Swap (item, items [swapIndex]);
				} else {
					return;
				}
			} else {
				return;
			}
		}
	}

	void SortUp(T item) {
		int parentIndex = (item.HeapIndex - 1) / 2;
		while (true) {
			T parentItem = items [parentIndex];
			if (item.CompareTo (parentItem) > 0) {
				Swap (item, parentItem);
			} else {
				break;
			}
			parentIndex = (item.HeapIndex - 1) / 2;
		}
	}

	void Swap(T itemA, T itemB) {
		items [itemA.HeapIndex] = itemB;
		items [itemB.HeapIndex] = itemA;
		int swapItem = itemA.HeapIndex;
		itemA.HeapIndex = itemB.HeapIndex;
		itemB.HeapIndex = swapItem;
	}
}

public interface IHeapItem<T> : IComparable<T> {
	int HeapIndex {
		get;
		set;
	}
}