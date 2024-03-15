using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AutomationTesting_CorConnect.DMS.Response
{

	[XmlRoot(ElementName = "corPurchaseOrderNumber")]
	public class CorPurchaseOrderNumber
	{

		[XmlAttribute(AttributeName = "nil")]
		public bool Nil { get; set; }
	}

	[XmlRoot(ElementName = "corPointOfSaleName")]
	public class CorPointOfSaleName
	{

		[XmlAttribute(AttributeName = "nil")]
		public bool Nil { get; set; }
	}

	[XmlRoot(ElementName = "corPointOfSaleAddress1")]
	public class CorPointOfSaleAddress1
	{

		[XmlAttribute(AttributeName = "nil")]
		public bool Nil { get; set; }
	}

	[XmlRoot(ElementName = "corPointOfSaleAddress2")]
	public class CorPointOfSaleAddress2
	{

		[XmlAttribute(AttributeName = "nil")]
		public bool Nil { get; set; }
	}

	[XmlRoot(ElementName = "corPointOfSaleCity")]
	public class CorPointOfSaleCity
	{

		[XmlAttribute(AttributeName = "nil")]
		public bool Nil { get; set; }
	}

	[XmlRoot(ElementName = "corPointOfSaleCountryCode")]
	public class CorPointOfSaleCountryCode
	{

		[XmlAttribute(AttributeName = "nil")]
		public bool Nil { get; set; }
	}

	[XmlRoot(ElementName = "corPointOfSale")]
	public class CorPointOfSale
	{

		[XmlElement(ElementName = "corPointOfSaleName")]
		public CorPointOfSaleName CorPointOfSaleName { get; set; }

		[XmlElement(ElementName = "corPointOfSaleAddress1")]
		public CorPointOfSaleAddress1 CorPointOfSaleAddress1 { get; set; }

		[XmlElement(ElementName = "corPointOfSaleAddress2")]
		public CorPointOfSaleAddress2 CorPointOfSaleAddress2 { get; set; }

		[XmlElement(ElementName = "corPointOfSaleCity")]
		public CorPointOfSaleCity CorPointOfSaleCity { get; set; }

		[XmlElement(ElementName = "corPointOfSaleCountryCode")]
		public CorPointOfSaleCountryCode CorPointOfSaleCountryCode { get; set; }
	}

	[XmlRoot(ElementName = "corComment")]
	public class CorComment
	{

		[XmlElement(ElementName = "corSectionCommentSequence")]
		public int CorSectionCommentSequence { get; set; }

		[XmlElement(ElementName = "corSectionCommentType")]
		public int CorSectionCommentType { get; set; }

		[XmlElement(ElementName = "corSectionComment")]
		public string CorSectionComment { get; set; }
	}

	[XmlRoot(ElementName = "corComments")]
	public class CorComments
	{

		[XmlElement(ElementName = "corComment")]
		public CorComment CorComment { get; set; }
	}

	[XmlRoot(ElementName = "corSection")]
	public class CorSection
	{

		[XmlElement(ElementName = "corSectionNumber")]
		public int CorSectionNumber { get; set; }

		[XmlElement(ElementName = "corComments")]
		public CorComments CorComments { get; set; }

		[XmlElement(ElementName = "corLineDetails")]
		public object CorLineDetails { get; set; }
	}

	[XmlRoot(ElementName = "corSections")]
	public class CorSections
	{

		[XmlElement(ElementName = "corSection")]
		public CorSection CorSection { get; set; }
	}

	[XmlRoot(ElementName = "corBaseImage")]
	public class CorBaseImage
	{

		[XmlAttribute(AttributeName = "nil")]
		public bool Nil { get; set; }
	}

	[XmlRoot(ElementName = "corResponseMessage")]
	public class CorResponseMessage
	{

		[XmlElement(ElementName = "corResponseMessageType")]
		public int CorResponseMessageType { get; set; }

		[XmlElement(ElementName = "corResponseMessageCode")]
		public int CorResponseMessageCode { get; set; }

		[XmlElement(ElementName = "corResponseMessageComment")]
		public string CorResponseMessageComment { get; set; }
	}

	[XmlRoot(ElementName = "corResponseMessages")]
	public class CorResponseMessages
	{

		[XmlElement(ElementName = "corResponseMessage")]
		public CorResponseMessage CorResponseMessage { get; set; }
	}

	[XmlRoot(ElementName = "corResponse")]
	public class CorResponse
	{

		[XmlElement(ElementName = "corRequestID")]
		public string CorRequestID { get; set; }

		[XmlElement(ElementName = "corRequestType")]
		public string CorRequestType { get; set; }

		[XmlElement(ElementName = "corVendorCode")]
		public string CorVendorCode { get; set; }

		[XmlElement(ElementName = "corCustomerCode")]
		public string CorCustomerCode { get; set; }

		[XmlElement(ElementName = "corCommunityCode")]
		public string CorCommunityCode { get; set; }

		[XmlElement(ElementName = "corAuthorizationCode")]
		public string CorAuthorizationCode { get; set; }

		[XmlElement(ElementName = "corTransactionType")]
		public string CorTransactionType { get; set; }

		[XmlElement(ElementName = "corTransactionNumber")]
		public string CorTransactionNumber { get; set; }

		[XmlElement(ElementName = "corTransactionDate")]
		public int CorTransactionDate { get; set; }

		[XmlElement(ElementName = "corPurchaseOrderNumber")]
		public CorPurchaseOrderNumber CorPurchaseOrderNumber { get; set; }

		[XmlElement(ElementName = "corTransactionAmount")]
		public int CorTransactionAmount { get; set; }

		[XmlElement(ElementName = "corAuthorizationAmount")]
		public int CorAuthorizationAmount { get; set; }

		[XmlElement(ElementName = "corCurrencyCode")]
		public string CorCurrencyCode { get; set; }

		[XmlElement(ElementName = "corAccelerationTerms")]
		public object CorAccelerationTerms { get; set; }

		[XmlElement(ElementName = "corPointOfSale")]
		public CorPointOfSale CorPointOfSale { get; set; }

		[XmlElement(ElementName = "corReferences")]
		public object CorReferences { get; set; }

		[XmlElement(ElementName = "corSections")]
		public CorSections CorSections { get; set; }

		[XmlElement(ElementName = "corTaxes")]
		public object CorTaxes { get; set; }

		[XmlElement(ElementName = "corBaseImage")]
		public CorBaseImage CorBaseImage { get; set; }

		[XmlElement(ElementName = "corResponseID")]
		public int CorResponseID { get; set; }

		[XmlElement(ElementName = "corResponseStatusCode")]
		public int CorResponseStatusCode { get; set; }

		[XmlElement(ElementName = "corResponseMessages")]
		public CorResponseMessages CorResponseMessages { get; set; }

		[XmlAttribute(AttributeName = "xsd")]
		public string Xsd { get; set; }

		[XmlAttribute(AttributeName = "xsi")]
		public string Xsi { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

}

