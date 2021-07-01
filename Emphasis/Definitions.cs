using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace Emphasis
{
	internal static class Definitions
	{
		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Comment.Note")]
		private static readonly ClassificationTypeDefinition ClassificationCommentTaskNote;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Comment.Todo")]
		private static readonly ClassificationTypeDefinition ClassificationCommentTaskTodo;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Comment.Temp")]
		private static readonly ClassificationTypeDefinition ClassificationCommentTaskTemp;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Comment.Hack")]
		private static readonly ClassificationTypeDefinition ClassificationCommentTaskHack;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Comment.Fixme")]
		private static readonly ClassificationTypeDefinition ClassificationCommentTaskFixme;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Comment.Rant")]
		private static readonly ClassificationTypeDefinition ClassificationCommentTaskRant;
	}
}
