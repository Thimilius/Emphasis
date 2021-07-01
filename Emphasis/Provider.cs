using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace Emphasis
{
	[Export(typeof(IClassifierProvider)), ContentType("C/C++"), ContentType("CSharp"), ContentType("JavaScript"), ContentType("TypeScript")]
	internal sealed class Provider : IClassifierProvider
	{
		[Import]
		private readonly IClassificationTypeRegistryService m_ClassificationTypeRegistryService;

		[Import]
		private readonly IClassifierAggregatorService m_ClassifierAggregatorService;

		private static bool m_IgnoreRequest;

		public IClassifier GetClassifier(ITextBuffer textBuffer)
		{
			if (m_IgnoreRequest) return null;

			try
			{
				m_IgnoreRequest = true;

				return textBuffer.Properties.GetOrCreateSingletonProperty
				(
					() => new Classifier
					(
						m_ClassificationTypeRegistryService,
						m_ClassifierAggregatorService.GetClassifier(textBuffer)
					)
				);
			}
			finally
			{
				m_IgnoreRequest = false;
			}
		}
	}
}
