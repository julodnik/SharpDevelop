//
// DuplicateIfInIfChainIssueTests.cs
//
// Author:
//       Ciprian Khlud <ciprian.mustiata@yahoo.com>
//
// Copyright (c) 2013 Ciprian Khlud
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using ICSharpCode.NRefactory.CSharp.Refactoring;
using NUnit.Framework;

namespace ICSharpCode.NRefactory.CSharp.CodeIssues
{
	[TestFixture]
	public class DuplicateIfInIfChainIssueTests : InspectionActionTestBase
	{
		[Test]
		public void TestSimple()
		{
			var input = @"
class TestClass
{
	void TestMethod (int i)
	{
		if(i > 0) {}else if (i > 0){}
	}
}";
			var output = @"
class TestClass
{
	void TestMethod (int i)
	{
		if (i > 0) {
		} 
	}
}";
			Test<DuplicateIfInIfChainIssue>(input, output);
		}

		[Test]
		public void Test()
		{
			var input = @"
class TestClass
{
	void TestMethod (int i)
	{
		if(i > 0) {
		} else 
		if (i % 2 == 0){
		} else 
		if (i > 0) {
		}
	}
}";
			var output = @"
class TestClass
{
	void TestMethod (int i)
	{
		if(i > 0) {
		} else if (i % 2 == 0) {
		} 
	}
}";
			Test<DuplicateIfInIfChainIssue>(input, output);
		}

		[Test]
		public void TestComplexWithMoreBranches()
		{
			var input = @"
class TestClass
{
	void TestMethod (int i)
	{
		if(i > 0) {
		} else 
		if (i % 2 == 0){
		} else 
		if (i > 0) {
		} else 
		if (i > 1) {
		}
	}
}";
			var output = @"
class TestClass
{
	void TestMethod (int i)
	{
		if(i > 0) {
		} else if (i % 2 == 0) {
		} else if (i > 1) {
		}
	}
}";
			Test<DuplicateIfInIfChainIssue>(input, output);
		}
	}
}