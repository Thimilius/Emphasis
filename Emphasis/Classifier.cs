using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Emphasis
{
	internal class Classifier : IClassifier
	{
		public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

		private readonly IClassificationType m_CommentNote;
		private readonly IClassificationType m_CommentTodo;
		private readonly IClassificationType m_CommentTemp;
		private readonly IClassificationType m_CommentHack;
		private readonly IClassificationType m_CommentFixme;
		private readonly IClassificationType m_CommentRant;

		private bool m_IsClassificationRunning;
		private readonly IClassifier m_Classifier;

		internal Classifier(IClassificationTypeRegistryService registry, IClassifier classifier)
		{
			m_IsClassificationRunning = false;
			m_Classifier = classifier;
			
			m_CommentNote = registry.GetClassificationType("Comment.Note");
			m_CommentTodo = registry.GetClassificationType("Comment.Todo");
			m_CommentTemp = registry.GetClassificationType("Comment.Temp");
			m_CommentHack = registry.GetClassificationType("Comment.Hack");
			m_CommentFixme = registry.GetClassificationType("Comment.Fixme");
			m_CommentRant = registry.GetClassificationType("Comment.Rant");
		}

		public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
		{
			if (m_IsClassificationRunning) return new List<ClassificationSpan>();

			try
			{
				m_IsClassificationRunning = true;
				return Classify(span);
			}
			finally
			{
				m_IsClassificationRunning = false;
			}
		}

		private IList<ClassificationSpan> Classify(SnapshotSpan span)
		{
			List<ClassificationSpan> spans = new List<ClassificationSpan>();

			if (span.IsEmpty)
				return spans;

			var text = span.GetText();

			var offset = 0;

			int currentOffset;

			l_NextComment:
			foreach (Match match in new Regex(@"(?<Star>\*)?" + @"(?<Slashes>(?<!/)(/{2,}))[ \t\v\f]*" + @"(?<Comment>[^\n]*)").Matches(text))
			{
				var starOffset = 0;

				if (match.Groups["Star"].Length > 0)
					goto SkipComment;

				var matchedSpan = new SnapshotSpan(span.Snapshot, new Span(span.Start + offset + starOffset + match.Index, match.Length - starOffset));
				var intersections = m_Classifier.GetClassificationSpans(matchedSpan);

				foreach (var Intersection in intersections)
				{
					var Classifications = Intersection.ClassificationType.Classification.Split(new[] { " - " }, StringSplitOptions.None);

					// Comment must be classified as either "comment" or "XML Doc Comment".
					if (!Utils.IsClassifiedAs(Classifications, new[] { PredefinedClassificationTypeNames.Comment, "XML Doc Comment" }))
						goto SkipComment;

					// Prevent recursive matching fragment of comment as another comment.
					if (Utils.IsClassifiedAs(Classifications, new[] { "Comment.Default" }))//, "Comment.Triple" }))
						goto SkipComment;
				}

				// Start offset of slashes (without star part: either "*" or "*/").
				int SlashesStart = span.Start + offset + match.Groups["Slashes"].Index;
				if (starOffset == 2) SlashesStart += 1;

				// Slashes length (optionally without first "/" as it's end of multiline comment).
				var SlashesLength = match.Groups["Slashes"].Length;
				if (starOffset == 2) SlashesLength -= 1;

				// If comment is triple slash (begins with "///").
				var IsTripleSlash = SlashesLength == 3;

				if (IsTripleSlash)
					goto SkipComment;

				var commentText = match.Groups["Comment"].Value;
				int commentStart = span.Start + offset + match.Groups["Comment"].Index;
				
				var skipInlineMatching = false;

				for (int i = 0; i < TaskManager.Count; i++)
				{
					var prefix = TaskManager.GetPrefix(i);
					if (commentText.ToLower().Trim().StartsWith(prefix.ToLower() + ":"))
					{
						spans.Add(new ClassificationSpan(new SnapshotSpan
						(
							span.Snapshot, new Span
							(
								commentStart,
								prefix.Length
							)
						), GetClassificationTypeForPrefix(prefix)));

						skipInlineMatching = true;
					}
				}

				if (skipInlineMatching)
					goto FinishClassification;

				FinishClassification:
				currentOffset = match.Index + match.Length;
				text = text.Substring(currentOffset);
				offset += currentOffset;
				goto l_NextComment;

				SkipComment:
				currentOffset =
						match.Groups["Slashes"].Index
					+ match.Groups["Slashes"].Length
				;

				text = text.Substring(currentOffset);
				offset += currentOffset;
				goto l_NextComment;
			}

			return spans;
		}

        private IClassificationType GetClassificationTypeForPrefix(string prefix)
        {
            switch (prefix)
            {
				case "note":
					return m_CommentNote;
				case "todo":
					return m_CommentTodo;
				case "temp":
					return m_CommentTemp;
				case "hack":
					return m_CommentHack;
				case "fixme":
					return m_CommentFixme;
				case "rant":
					return m_CommentRant;
				default:
					throw new Exception();
            }
        }
    }
}
