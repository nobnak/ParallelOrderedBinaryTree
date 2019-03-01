using nobnak.Gist;
using nobnak.Gist.MathAlgorithms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParallelOrderedBinaryTreeSystem {

	public class OBT {

		protected IList<uint> keys;
		protected List<uint> prefixes = new List<uint>();
		protected List<uint> splits = new List<uint>();

		public OBT(IList<uint> keys) {
			Build(keys);
		}

		#region interface
		public void Build(IList<uint> keys) {
			this.keys = keys;
			splits.Clear();
			splits.AddRange(keys);
			prefixes.Clear();
			prefixes.AddRange(keys);

			Parallel.For(0, keys.Count -1, BuildEach);
		}
		public IList<uint> Prefixes { get { return new List<uint>(prefixes); } }
		public IList<uint> Splits { get { return new List<uint>(splits); } }
		#endregion

		#region member
		protected int LengthOfLCP(int a, int b) {
			if (a < 0 || keys.Count <= a || b < 0 || keys.Count <= b)
				return -1;
			var ak = keys[a];
			var bk = keys[b];
			return ak.LengthOfLCP(bk);
		}
		protected void BuildEach(int i) {
			var d = System.Math.Sign(LengthOfLCP(i, i + 1) - LengthOfLCP(i, i - 1));

			// limit
			var smin = LengthOfLCP(i, i - d);
			var lmax = 2;
			while (LengthOfLCP(i, i + lmax * d) > smin)
				lmax *= 2;

			// other end
			var l = 0;
			for (var t = lmax >> 1; t > 0; t >>= 1)
				if (LengthOfLCP(i, i + (l + t) * d) > smin)
					l += t;
			var j = i + l * d;

			// split node
			var snode = LengthOfLCP(i, j);
			var s = 0;
			for (var t = l >> 1; t > 0; t >>= 1)
				if (LengthOfLCP(i, i + (s + t) * d) > snode)
					s += t;
			var g = i + s * d + Mathf.Min(d, 0);

			prefixes[i] = keys[i].PrefixByLength(snode);
			splits[i] = (uint)g;
		}
		#endregion
	}
}
