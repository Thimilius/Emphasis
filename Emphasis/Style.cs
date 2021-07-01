using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using Color = System.Windows.Media.Color;

namespace Emphasis
{
	internal static class Colors
	{
		internal static readonly Color Note = Color.FromRgb(0, 168, 69);
		internal static readonly Color Todo = Color.FromRgb(255, 232, 132);
		internal static readonly Color Temp = Color.FromRgb(0, 180, 237);
		internal static readonly Color Hack = Color.FromRgb(223, 132, 0);
		internal static readonly Color Fixme = Color.FromRgb(202, 0, 0);
		internal static readonly Color Rant = Color.FromRgb(202, 0, 0);
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Comment.Note")]
	[Name("Comment.Note")]
	[BaseDefinition(PredefinedClassificationTypeNames.Comment)]
	[UserVisible(true)]
	[Order(After = PredefinedClassificationTypeNames.Comment)]
	[Order(After = "XML Doc Comment")]
	internal sealed class FormatCommentTaskNote : ClassificationFormatDefinition
	{
		public FormatCommentTaskNote()
		{
			DisplayName = "Comment Task Note";

			BackgroundCustomizable = false;
			ForegroundColor = Colors.Note;
			IsBold = true;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Comment.Todo")]
	[Name("Comment.Todo")]
	[BaseDefinition(PredefinedClassificationTypeNames.Comment)]
	[UserVisible(true)]
	[Order(After = PredefinedClassificationTypeNames.Comment)]
	[Order(After = "XML Doc Comment")]
	internal sealed class FormatCommentTaskTodo : ClassificationFormatDefinition
	{
		public FormatCommentTaskTodo()
		{
			DisplayName = "Comment Task Todo";

			BackgroundCustomizable = false;
			ForegroundColor = Colors.Todo;
			IsBold = true;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Comment.Temp")]
	[Name("Comment.Temp")]
	[BaseDefinition(PredefinedClassificationTypeNames.Comment)]
	[UserVisible(true)]
	[Order(After = PredefinedClassificationTypeNames.Comment)]
	[Order(After = "XML Doc Comment")]
	internal sealed class FormatCommentTaskTemp : ClassificationFormatDefinition
	{
		public FormatCommentTaskTemp()
		{
			DisplayName = "Comment Task Temp";

			BackgroundCustomizable = false;
			ForegroundColor = Colors.Temp;
			IsBold = true;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Comment.Hack")]
	[Name("Comment.Hack")]
	[BaseDefinition(PredefinedClassificationTypeNames.Comment)]
	[UserVisible(true)]
	[Order(After = PredefinedClassificationTypeNames.Comment)]
	[Order(After = "XML Doc Comment")]
	internal sealed class FormatCommentTaskHack : ClassificationFormatDefinition
	{
		public FormatCommentTaskHack()
		{
			DisplayName = "Comment Task Hack";

			BackgroundCustomizable = false;
			ForegroundColor = Colors.Hack;
			IsBold = true;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Comment.Fixme")]
	[Name("Comment.Fixme")]
	[BaseDefinition(PredefinedClassificationTypeNames.Comment)]
	[UserVisible(true)]
	[Order(After = PredefinedClassificationTypeNames.Comment)]
	[Order(After = "XML Doc Comment")]
	internal sealed class FormatCommentTaskFixme : ClassificationFormatDefinition
	{
		public FormatCommentTaskFixme()
		{
			DisplayName = "Comment Task Fixme";

			BackgroundCustomizable = false;
			ForegroundColor = Colors.Fixme;
			IsBold = true;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Comment.Rant")]
	[Name("Comment.Rant")]
	[BaseDefinition(PredefinedClassificationTypeNames.Comment)]
	[UserVisible(true)]
	[Order(After = PredefinedClassificationTypeNames.Comment)]
	[Order(After = "XML Doc Comment")]
	internal sealed class FormatCommentTaskRant : ClassificationFormatDefinition
	{
		public FormatCommentTaskRant()
		{
			DisplayName = "Comment Task Rant";

			BackgroundCustomizable = false;
			ForegroundColor = Colors.Rant;
			IsBold = true;
		}
	}
}
