using NUnit.Framework;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ParallelOrderedBinaryTreeSystem {

	public class TestOBT {

		public static readonly uint[] ORDER10 = Enumerable.Range(0, 10).Select(v => (uint)v).ToArray();
		public static readonly uint[] SPLIT10 = new uint[] { 7, 0, 2, 1, 5, 4, 6, 3, 8, 9 };

		[Test]
		public void TestBuild() {
			var obt = new OBT(ORDER10);
			var splits = obt.Splits;
			for (var i = 0; i < SPLIT10.Length - 1; i++)
				Assert.AreEqual(SPLIT10[i], splits[i]);
		}
	}
}
