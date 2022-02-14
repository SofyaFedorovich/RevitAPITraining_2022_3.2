using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITraining_2022_3._2
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            IList<Reference> selectedElementReferenceList = uidoc.Selection.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element, "Выберите элементы");
            var elementList = new List<Element>();

            double SumLength = 0;

            foreach (var selectedElement in selectedElementReferenceList)
            {
                Element element = doc.GetElement(selectedElement);
                elementList.Add(element);

                if (selectedElement is MEPCurve)
                {
                    Parameter pipelength1 = selectedElement.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH);
                    if (pipelength1.StorageType == StorageType.Double)
                    {
                     SumLength += pipelength1.AsDouble();
                    }
                }

            }

            TaskDialog.Show("Суммарная длина труб", SumLength.ToString());
            return Result.Succeeded;
        }
    }
}
