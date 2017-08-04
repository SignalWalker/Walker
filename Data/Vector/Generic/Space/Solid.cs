﻿namespace Walker.Data.Vector.Generic.Space {
	public interface Solid<T> {

		bool Contains(Vector3<T> vec);

		bool Intersects(Solid<T> sol);

	}
}