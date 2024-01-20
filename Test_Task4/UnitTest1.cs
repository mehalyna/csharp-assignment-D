using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;


namespace Test_Task1
{
    [TestClass]
    public class UnitTest1
    {
        #region Initialize

        #endregion

        [TestMethod, TestCategory("Task4")]
        [TestCategory("InOut")]
        [TestProperty("GSO-DevGroup", "Akinci")]
        [DataRow("1", "-1", "0", "10", "Avinash", "Nilay","2")]
        [DataRow("1", "eingabe", "0", "8", "Avinash", "Nilay","2")]

        public void Test_InOut1(string value_1, string value_2, string value_3, string value_4, string value_5, string value_6)
        {
            // Arrange
            var writer = new StringWriter();
            Console.SetOut(writer);

            var textReader = new StringReader(@$"{value_1}
{value_2}
{value_3}
{value_4}
{value_5}
{value_6}
{"3"}");

            Test_InOutcheck(writer, textReader, 1, value_4, value_5,value_6) ;

        }

        [TestMethod, TestCategory("Task4")]
        [TestCategory("InOut")]
        [TestProperty("GSO-DevGroup", "Akinci")]
        [DataRow("1", "10", "Avinash", "Nilay")]

        public void Test_InOut2(string value_1, string value_2, string value_3, string value_4)
        {
            // Arrange
            var writer = new StringWriter();
            Console.SetOut(writer);

            var textReader = new StringReader(@$"{value_1}
{value_2}
{value_3}
{value_4}
{"3"}");

            Test_InOutcheck(writer,textReader, 2,value_2,value_3,value_4);

        }

        [TestMethod, TestCategory("Task4")]
        [TestCategory("InOut")]
        [TestProperty("GSO-DevGroup", "Akinci")]
        [DataRow("4")]
        [DataRow("test")]

        public void Test_InOut3(string value_1)
        {
            // Arrange
            var writer = new StringWriter();
            Console.SetOut(writer);

            var textReader = new StringReader(@$"{value_1}
{"1"}{"3"}");

            Test_InOutcheck(writer, textReader, 2);

        }


        private static void Test_InOutcheck(StringWriter writer, StringReader textReader, int test,string ZN="",string VN="", string NN="")
        {
            bool exit=false;
            Console.SetIn(textReader);
            // Act
            try
            {
                Aufgabe_4.Aufgabe4();
            }
            catch (System.Reflection.TargetParameterCountException ex)
            {
                exit=true;  
            }
            catch (System.IO.IOException ex)
            {
                exit = true;
            }
            if (!exit)
            {
                // Assert

                var sb = writer.GetStringBuilder();
                var lines = sb.ToString().Split(Environment.NewLine, StringSplitOptions.None);

                List<string> lines_list = new List<string>();

                //Bedingung nötig da 'Enviroment.NewLine' in Git Actions nicht funktioniert.
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i] != "")
                    {
                        lines_list.Add(lines[i]);
                        Debug.WriteLine($"{lines[i]}");
                    }
                }

                List<string> lines_list_check;
                if (test == 1)
                {
                    lines_list_check = new List<string> { $"Zimmernummer:{ZN}", $"Vorname:{VN}", $"Nachname:{NN}" };
                }
                else if (test == 2)
                {
                    lines_list_check = new List<string> { $"Zimmernummer:{ZN}", $"Vorname:{VN}", $"Nachname:{NN}" };
                }
                else
                {
                    lines_list_check = new List<string> { $"Falsche Eingabe..." };

                }


                lines_list = lines_list.Intersect(lines_list_check).ToList();


                for (int i = 0; i < lines_list_check.Count; i++)
                {

                    try
                    {
                        if (lines_list[i] != lines_list_check[i]) Trace.WriteLine($"\nFehler: '{lines_list_check[i]}' nicht gefunden");
                        Assert.AreEqual(lines_list[i], lines_list_check[i]);
                    }
                    catch
                    {
                        Trace.WriteLine($"Fehler: Zeilen fehlen");
                        Assert.Fail(); ;
                    }

                }
            }
            else
            {
                Assert.IsTrue(true);
            }
            
        }


    }
}
