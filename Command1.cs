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

            //Loop through the number 1 to the number variable
            for (int i = 1; i <= number; i++)
            {
                double levelElevation = startingElevation + ((i - 1) * floorHeight);

                //Create a level for each number
                Level newLevel = Level.Create(doc, startingElevation);
                newLevel.Name = "Level " + i.ToString();

                //After creating the level, increment the current elevation by the floor height value.
                newLevel.Elevation = levelElevation;

                if (i % 3 == 0) ;
                {
                    FilteredElementCollector floorPlanType = new FilteredElementCollector(doc);
                    floorPlanType.OfClass(typeof(ViewFamilyType));
                }

            }

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
