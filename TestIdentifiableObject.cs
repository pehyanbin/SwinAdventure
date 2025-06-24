using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using SwinAdventure;

namespace TestIdentifiableObject
{
    [TestFixture]
    public class TestIdentifiableObject
    {
        public string stuid = "105507551";
        public string firstname = "yan bin";
        public string familyname = "peh";

        //test case 1
        [Test]
        public void TestAreYou()
        {
            IdentifiableObject idenObj = new IdentifiableObject(new string[] { stuid, firstname, familyname});
            ClassicAssert.IsTrue(idenObj.AreYou("105507551"), "Identify Student ID Test");
            ClassicAssert.IsTrue(idenObj.AreYou("yan bin"), "Identify First Name Test");
            ClassicAssert.IsTrue(idenObj.AreYou("peh"), "Identify Last Name Test");
        }

        //test case 2
        [Test]
        public void TestNotAreYou()
        {
            IdentifiableObject idenObj = new IdentifiableObject(new string[] { stuid, firstname, familyname });
            ClassicAssert.IsFalse(idenObj.AreYou("1055O7551"), "Identify if it is not matched with Student ID Test");
            ClassicAssert.IsFalse(idenObj.AreYou("George"), "Identify if it is not matched with First Name or Last Name Test");
        }

        //test case 3
        [Test]
        public void TestCaseSensitive()
        {
            IdentifiableObject idenObj = new IdentifiableObject(new string[] { firstname, familyname});

            string value = "Yan Bin";
            string valueUp = value.ToUpper();
            ClassicAssert.IsTrue(idenObj.AreYou(valueUp), "Identify uppercase in first name Test");
            ClassicAssert.IsTrue(idenObj.AreYou(value), "Identify mixed of uppercase and lowercase in first name Test");

            string value2 = "Peh";
            string value2Up = value2.ToUpper();
            ClassicAssert.IsTrue(idenObj.AreYou(value2Up), "Identify uppercase in last name Test");
            ClassicAssert.IsTrue(idenObj.AreYou(value2), "Identify mixed of uppercase and lowercase in last name Test");
        }

        //test case 4
        [Test]
        public void TestFirstId()
        {
            IdentifiableObject idenObj = new IdentifiableObject(new string[] { stuid, firstname, familyname});
            ClassicAssert.AreEqual("105507551", idenObj.FirstId, "First ID = Student ID Test");

            IdentifiableObject idenObj2 = new IdentifiableObject(new string[] {firstname, familyname});
            ClassicAssert.AreEqual("yan bin", idenObj2.FirstId, "First ID = lowercase First Name Test");
        }

        //test case 5
        [Test]
        public void TestFirstId_NoId()
        {
            IdentifiableObject idenObj = new IdentifiableObject(indents: null);
            ClassicAssert.AreEqual("", idenObj.FirstId, "First ID for null input : empty Test");

            IdentifiableObject idenObj2 = new IdentifiableObject(new string[] { });
            ClassicAssert.AreEqual("", idenObj2.FirstId, "First ID for empty array : empty Test");
        }

        //test case 6
        [Test]
        public void TestAddId()
        {
            IdentifiableObject idenObj = new IdentifiableObject(new string[] { stuid, firstname });
            string value = "Peh";
            idenObj.AddIdentifier(value);
            string valueUp = value.ToUpper();

            ClassicAssert.IsTrue(idenObj.AreYou(value), "Identify added name Test");
            ClassicAssert.IsTrue(idenObj.AreYou(familyname), "Identify added lowercase name Test");
            ClassicAssert.IsTrue(idenObj.AreYou(valueUp), "Identify added uppercase name Test");
        }

        //test case 7
        [Test]
        public void TestPrivilegeEscalation()
        {
            IdentifiableObject idenObj = new IdentifiableObject(new string[] { "105507551", "yan bin", "peh" });

            ClassicAssert.AreEqual("105507551", idenObj.FirstId, "Original First Id = Student ID");
            ClassicAssert.IsTrue(idenObj.AreYou("105507551"), "Identify original Student ID");
            ClassicAssert.IsTrue(idenObj.AreYou("yan bin"), "Identify original First Name");
            ClassicAssert.IsTrue(idenObj.AreYou("peh"), "Identify original Family Name");

            idenObj.PrivilegeEscalation("6666");
            ClassicAssert.AreEqual("105507551", idenObj.FirstId, "First Id remains. ( if wrong pin ) ");
            ClassicAssert.IsTrue(idenObj.AreYou("105507551"), "Still identify Student Id ( wrong pin situation )");

            idenObj.PrivilegeEscalation("7551");
            ClassicAssert.AreEqual("20007", idenObj.FirstId, "First Id = tutorial id after escalation");
            ClassicAssert.IsTrue(idenObj.AreYou("20007"), "Identify tutorial id after escalation.");
            ClassicAssert.IsFalse(idenObj.AreYou("105507551"), "Identify old Student Id after escalation.");
            ClassicAssert.IsTrue(idenObj.AreYou("yan bin"), "Identify First Name after escalation.");
            ClassicAssert.IsTrue(idenObj.AreYou("peh"), "Identify Last Name after escalation.");

        }


        //test case 8
        
        [Test]
        public void TestPrivilegeEscalation_NoId()
        {
            IdentifiableObject idenObj = new IdentifiableObject(new string[] { });
            idenObj.PrivilegeEscalation("7551");
            ClassicAssert.AreEqual("", idenObj.FirstId, "First Id is empty without no identifiers ");
            ClassicAssert.IsFalse(idenObj.AreYou("20007"), "Not identifying tutorial id without identifiers ");
        }
        
    }
}
