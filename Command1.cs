namespace RAA_Module_01_Skills
{
    [Transaction(TransactionMode.Manual)]
    public class Command1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // this is a variable for the Revit application
            UIApplication uiapp = commandData.Application;

            // this is a variable for the current Revit model
            Document doc = uiapp.ActiveUIDocument.Document;

            // Your code goes here
            Transaction t = new Transaction(doc);
            t.Start("Module 1 Skills");

            //Declare a number variable and set it to 250
            int number = 250;

            //Declare a starting elevation variable and set it to 0
            double startingElevation = 0;

            //Declare a floor height variable and set it to 15 
            double floorHeight = 15;

            //Count FIZZ, BUZZ & FIZZBUZZ to report in TaskDialog - intialize variables
            int countFIZZ = 0;
            int countBUZZ = 0;
            int countFIZZBUZZ = 0;

            //Loop through the number 1 to the number variable
            for (int i = 1; i <= number; i++)
            {
                double levelElevation = startingElevation + ((i - 1) * floorHeight);

                //Create a level for each number
                Level newLevel = Level.Create(doc, startingElevation);
                newLevel.Name = "Level_" + i.ToString();

                //After creating the level, increment the current elevation by the floor height value.
                newLevel.Elevation = levelElevation;



                //If the number is divisible by both 3 and 5, create a sheet and name it "FIZZBUZZ_#"
                if (i % 3 == 0 && i % 5 == 0)
                {
                    FilteredElementCollector titleblockCollector = new FilteredElementCollector(doc);
                    titleblockCollector.OfCategory(BuiltInCategory.OST_TitleBlocks);


                    ViewSheet newSheet = ViewSheet.Create(doc, titleblockCollector.FirstElementId());
                    newSheet.Name = "FIZZBUZZ_" + i.ToString();                    

                    List<ViewSheet> fBSheetList = new List<ViewSheet>();
                   
                    for (int a  = 1; a <= fBSheetList.Count; a++)
                    {
                        newSheet.SheetNumber = "A" + a;
                    }

                    //In addition to creating a sheet, create a floor plan for each FIZZBUZZ. Next, add the floor plan to the sheet by creating a Viewport element. 

                    FilteredElementCollector floorVFTCollector = new FilteredElementCollector(doc);
                    floorVFTCollector.OfClass(typeof(ViewFamilyType));

                    ViewFamilyType floorPlanVFT = null;
                    foreach (ViewFamilyType currentVFT in floorVFTCollector)
                    {
                        if (currentVFT.ViewFamily == ViewFamily.FloorPlan)
                        {
                            floorPlanVFT = currentVFT;
                            break;
                        }
                    }

                    ViewPlan planName = ViewPlan.Create(doc, floorPlanVFT.Id, newLevel.Id);
                    planName.Name = "FIZZBUZZ_" + i.ToString();

                    XYZ insPoint = new XYZ(1.5, .75, 0);
                    Viewport newView = Viewport.Create(doc, newSheet.Id, planName.Id, insPoint);

                    countFIZZBUZZ++;

                }

                //If the number is divisible by 3, create a floor plan and name it "FIZZ_#"

                else if (i % 3 == 0)
                {
                    FilteredElementCollector floorVFTCollector = new FilteredElementCollector(doc);
                    floorVFTCollector.OfClass(typeof(ViewFamilyType));

                    ViewFamilyType floorPlanVFT = null;
                    foreach (ViewFamilyType currentVFT in floorVFTCollector)
                    {
                        if (currentVFT.ViewFamily == ViewFamily.FloorPlan)
                        {
                            floorPlanVFT = currentVFT;
                            break;
                        }
                    }

                    ViewPlan planName = ViewPlan.Create(doc, floorPlanVFT.Id, newLevel.Id);
                    planName.Name = "FIZZ_" + i.ToString();

                    countFIZZ++;
                }

                //If the number is divisible by 5, create a ceiling plan and name it "BUZZ_#"

                else if (i % 5 == 0)
                {
                    FilteredElementCollector ceilingVFTCollector = new FilteredElementCollector(doc);
                    ceilingVFTCollector.OfClass(typeof(ViewFamilyType));

                    ViewFamilyType ceilingPlanVFT = null;
                    foreach (ViewFamilyType currentVFT in ceilingVFTCollector)
                    {
                        if (currentVFT.ViewFamily == ViewFamily.CeilingPlan)
                        {
                            ceilingPlanVFT = currentVFT;
                            break;
                        }
                    }

                    ViewPlan planName = ViewPlan.Create(doc, ceilingPlanVFT.Id, newLevel.Id);
                    planName.Name = "BUZZ_" + i.ToString();

                    countBUZZ++;

                }


            }

            int[] counts = [countFIZZBUZZ, countFIZZ, countBUZZ];
            TaskDialog.Show("FIZZBZZ Counter", "Sheet Count: " + counts[0].ToString() + System.Environment.NewLine + "Floor Plan Counter: " + counts[1].ToString() + System.Environment.NewLine + "Ceiling Plan Counter: " + counts[2].ToString());

            t.Commit();
            t.Dispose();

            return Result.Succeeded;
        }
        internal static PushButtonData GetButtonData()
        {
            // use this method to define the properties for this command in the Revit ribbon
            string buttonInternalName = "btnCommand1";
            string buttonTitle = "Button 1";
            string? methodBase = MethodBase.GetCurrentMethod().DeclaringType?.FullName;

            if (methodBase == null)
            {
                throw new InvalidOperationException("MethodBase.GetCurrentMethod().DeclaringType?.FullName is null");
            }
            else
            {
                Common.ButtonDataClass myButtonData1 = new Common.ButtonDataClass(
                    buttonInternalName,
                    buttonTitle,
                    methodBase,
                    Properties.Resources.Blue_32,
                    Properties.Resources.Blue_16,
                    "This is a tooltip for Button 1");

                return myButtonData1.Data;
            }
        }
    }

}
