using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AutomationTesting_CorConnect.DMS.RequestObjects
{
    //internal class Objects
    //{
		// using System.Xml.Serialization;
		// XmlSerializer serializer = new XmlSerializer(typeof(ProcessRequest));
		// using (StringReader reader = new StringReader(xml))
		// {
		//    var test = (ProcessRequest)serializer.Deserialize(reader);
		// }

		[XmlRoot(ElementName = "corAccelerationTerms")]
		public class CorAccelerationTerms
		{

			[XmlElement(ElementName = "corAccelerationProgramID")]
			public int CorAccelerationProgramID { get; set; }
		}

		[XmlRoot(ElementName = "corPointOfSale")]
		public class CorPointOfSale
		{

			[XmlElement(ElementName = "corPointOfSaleName")]
			public object CorPointOfSaleName { get; set; }

			[XmlElement(ElementName = "corPointOfSaleAddress1")]
			public object CorPointOfSaleAddress1 { get; set; }

			[XmlElement(ElementName = "corPointOfSaleAddress2")]
			public object CorPointOfSaleAddress2 { get; set; }

			[XmlElement(ElementName = "corPointOfSaleCity")]
			public string CorPointOfSaleCity { get; set; }

			[XmlElement(ElementName = "corPointOfSaleStateProvince")]
			public object CorPointOfSaleStateProvince { get; set; }

			[XmlElement(ElementName = "corPointOfSalePostalCode")]
			public object CorPointOfSalePostalCode { get; set; }

			[XmlElement(ElementName = "corPointOfSaleCountryCode")]
			public object CorPointOfSaleCountryCode { get; set; }
		}

		[XmlRoot(ElementName = "corReference")]
		public class CorReference
		{

			[XmlElement(ElementName = "corReferenceType")]
			public string CorReferenceType { get; set; }

			[XmlElement(ElementName = "corReferenceValue")]
			public string CorReferenceValue { get; set; }
		}

		[XmlRoot(ElementName = "corReferences")]
		public class CorReferences
		{

			[XmlElement(ElementName = "corReference")]
			public CorReference CorReference { get; set; }
		}

		[XmlRoot(ElementName = "corComment")]
		public class CorComment
		{

			[XmlElement(ElementName = "corSectionCommentSequence")]
			public string CorSectionCommentSequence { get; set; }

			[XmlElement(ElementName = "corSectionCommentType")]
			public string CorSectionCommentType { get; set; }

			[XmlElement(ElementName = "corSectionComment")]
			public string CorSectionComment { get; set; }
		}

		[XmlRoot(ElementName = "corComments")]
		public class CorComments
		{

			[XmlElement(ElementName = "corComment")]
			public CorComment CorComment { get; set; }
		}

		[XmlRoot(ElementName = "corPartCategory")]
		public class CorPartCategory
		{

			[XmlElement(ElementName = "corCategoryType")]
			public object CorCategoryType { get; set; }

			[XmlElement(ElementName = "corCategory")]
			public object CorCategory { get; set; }
		}

		[XmlRoot(ElementName = "corPartCategories")]
		public class CorPartCategories
		{

			[XmlElement(ElementName = "corPartCategory")]
			public CorPartCategory CorPartCategory { get; set; }
		}

		[XmlRoot(ElementName = "corLineDetailNotes")]
		public class CorLineDetailNotes
		{

			[XmlElement(ElementName = "corLineDetailNote")]
			public List<object> CorLineDetailNote { get; set; }
		}

		[XmlRoot(ElementName = "corLineDetail")]
		public class CorLineDetail
		{

			[XmlElement(ElementName = "corLineDetailSequence")]
			public int CorLineDetailSequence { get; set; }

			[XmlElement(ElementName = "corLineDetailType")]
			public string CorLineDetailType { get; set; }

			[XmlElement(ElementName = "corLineDetailItem")]
			public string CorLineDetailItem { get; set; }

			[XmlElement(ElementName = "corLineDetailManufacturerCode")]
			public object CorLineDetailManufacturerCode { get; set; }

			[XmlElement(ElementName = "corLineDetailDescription")]
			public string CorLineDetailDescription { get; set; }

			[XmlElement(ElementName = "corLineDetailVMRSCode")]
			public object CorLineDetailVMRSCode { get; set; }

			[XmlElement(ElementName = "corPartCategories")]
			public CorPartCategories CorPartCategories { get; set; }

			[XmlElement(ElementName = "corLineDetailQuantity")]
			public int CorLineDetailQuantity { get; set; }

			[XmlElement(ElementName = "corLineDetailUnitPrice")]
			public double CorLineDetailUnitPrice { get; set; }

			[XmlElement(ElementName = "corLineDetailCorePrice")]
			public double CorLineDetailCorePrice { get; set; }

			[XmlElement(ElementName = "corLineDetailFET")]
			public int CorLineDetailFET { get; set; }

			[XmlElement(ElementName = "corLineDetailNotes")]
			public CorLineDetailNotes CorLineDetailNotes { get; set; }

			[XmlElement(ElementName = "corLineDetailUOM")]
			public string CorLineDetailUOM { get; set; }
		}

		[XmlRoot(ElementName = "corLineDetails")]
		public class CorLineDetails
		{

			[XmlElement(ElementName = "corLineDetail")]
			public List<CorLineDetail> CorLineDetail { get; set; }
		}

		[XmlRoot(ElementName = "corSection")]
		public class CorSection
		{

			[XmlElement(ElementName = "corSectionNumber")]
			public string CorSectionNumber { get; set; }

			[XmlElement(ElementName = "corComments")]
			public CorComments CorComments { get; set; }

			[XmlElement(ElementName = "corLineDetails")]
			public CorLineDetails CorLineDetails { get; set; }
		}

		[XmlRoot(ElementName = "corSections")]
		public class CorSections
		{

			[XmlElement(ElementName = "corSection")]
			public List<CorSection> CorSection { get; set; }
		}

		[XmlRoot(ElementName = "corTax")]
		public class CorTax
		{

			[XmlElement(ElementName = "corTaxType")]
			public string CorTaxType { get; set; }

			[XmlElement(ElementName = "corTaxAmount")]
			public int CorTaxAmount { get; set; }

			[XmlElement(ElementName = "corTaxID")]
			public string CorTaxID { get; set; }
		}

		[XmlRoot(ElementName = "corTaxes")]
		public class CorTaxes
		{

			[XmlElement(ElementName = "corTax")]
			public CorTax CorTax { get; set; }
		}

		[XmlRoot(ElementName = "corRequest")]
		public class CorRequest
		{

			[XmlElement(ElementName = "corRequestID")]
			public double CorRequestID { get; set; }

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
			public string CorTransactionDate { get; set; }

			[XmlElement(ElementName = "corPurchaseOrderNumber")]
			public string CorPurchaseOrderNumber { get; set; }

			[XmlElement(ElementName = "corPurchaseOrderDate")]
			public object CorPurchaseOrderDate { get; set; }

			[XmlElement(ElementName = "corTransactionAmount")]
			public double CorTransactionAmount { get; set; }

			[XmlElement(ElementName = "corAuthorizationAmount")]
			public double CorAuthorizationAmount { get; set; }

			[XmlElement(ElementName = "corCurrencyCode")]
			public string CorCurrencyCode { get; set; }

			[XmlElement(ElementName = "corAccelerationTerms")]
			public CorAccelerationTerms CorAccelerationTerms { get; set; }

			[XmlElement(ElementName = "corPointOfSale")]
			public CorPointOfSale CorPointOfSale { get; set; }

			[XmlElement(ElementName = "corReferences")]
			public CorReferences CorReferences { get; set; }

			[XmlElement(ElementName = "corSections")]
			public CorSections CorSections { get; set; }

			[XmlElement(ElementName = "corTaxes")]
			public CorTaxes CorTaxes { get; set; }

			[XmlElement(ElementName = "corBaseImage")]
			public object CorBaseImage { get; set; }
		}

		[XmlRoot(ElementName = "ProcessRequest")]
		public class ProcessRequest
		{

			[XmlElement(ElementName = "UserName")]
			public string UserName { get; set; }

			[XmlElement(ElementName = "Password")]
			public string Password { get; set; }

			[XmlElement(ElementName = "corRequest")]
			public CorRequest CorRequest { get; set; }
		}

	public class Utf8StringWriter : StringWriter
	{
		public override Encoding Encoding => Encoding.UTF8;
	}
}

