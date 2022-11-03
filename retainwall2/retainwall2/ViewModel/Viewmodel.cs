using System.ComponentModel;
using System.Runtime.CompilerServices;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.Runtime;


namespace retainwall2
{
    public class Viewmodel : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void onprochanged([CallerMemberName] string newname = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(newname));
            }
        }
        //trong vm định nghĩa các props cần thiết.
        //set giá trị khở tạo cho nó trong ctor của vm nếu có

        public double Length { get; set; }
        public double Lengthb { get; set; }
        public double Lengthc { get; set; }
        public double Lengthd { get; set; }
        public double Lengthe { get; set; }
        public double Lengthf { get; set; }
        public double Lengthg { get; set; }




        //vd
        public Viewmodel()
        {
            //Khỏi tạo inti prop value ở đây
            Length = 400;
            Lengthb = 200;
            Lengthc = 2000;
            Lengthd = 500;
            Lengthe = 1000;
            Lengthf = 500;
            Lengthg = 1000;
        }


        //Viết thêm các hàm sử lý các sự kiện ở đây
        //vd click 0K từ view thì gọi DrawLine
        public void DrawLine()
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;

            // Phải lock document trước khi lấy dữ liệu từ View để vẽ
            using (Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                Transaction tran = db.TransactionManager.StartTransaction();


                try
                {
                    #region TẠO LAYER
                    // Tạo đối tượng layer trong Autocad
                    LayerTable acLyrTbl;
                    acLyrTbl = tran.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;

                    // Tạo layer thứ 1
                    string sLayerName = "0.35";
                    if (acLyrTbl.Has(sLayerName) == false)
                    {

                        LayerTableRecord acLyrTblRec = new LayerTableRecord();
                        // Assign the layer the ACI color 1 and a name
                        acLyrTblRec.Color = Color.FromColorIndex(ColorMethod.ByAci, 1);
                        acLyrTblRec.Name = sLayerName;
                        acLyrTblRec.LineWeight = LineWeight.LineWeight035;
                        // Upgrade the Layer table for write
                        acLyrTbl.UpgradeOpen();
                        // Append the new layer to the Layer table and the transaction
                        acLyrTbl.Add(acLyrTblRec);
                        tran.AddNewlyCreatedDBObject(acLyrTblRec, true);
                    }

                    // Tạo layer thứ 2
                    string sLayerName2 = "0.25";
                    if (acLyrTbl.Has(sLayerName2) == false)
                    {
                        LayerTableRecord acLyrTblRec2 = new LayerTableRecord();
                        // Assign the layer the ACI color 1 and a name
                        acLyrTblRec2.Color = Color.FromColorIndex(ColorMethod.ByAci, 2);
                        acLyrTblRec2.Name = sLayerName2;
                        acLyrTblRec2.LineWeight = LineWeight.LineWeight025;
                        // Upgrade the Layer table for write
                        acLyrTbl.UpgradeOpen();
                        // Append the new layer to the Layer table and the transaction
                        acLyrTbl.Add(acLyrTblRec2);
                        tran.AddNewlyCreatedDBObject(acLyrTblRec2, true);
                    }
                    #endregion

                    // Tạo không gian (đối tượng bản vẽ) để lưu trữ các đường line, pline, text...
                    ObjectId blockid = db.CurrentSpaceId;
                    BlockTableRecord tbrecord = tran.GetObject(blockid, OpenMode.ForWrite) as BlockTableRecord;

                    // Tạo Point 
                    Point2d po1 = new Point2d(0, 0);
                    Point2d po2 = new Point2d(Length, 0);
                    Point2d po3 = new Point2d(Length, -Lengthb);
                    Point2d po4 = new Point2d(Length, -Lengthb - Lengthc);
                    Point2d po5 = new Point2d(Length + Lengthd, -Lengthb - Lengthc);
                    Point2d po6 = new Point2d(Length + Lengthd, -Lengthb - Lengthc - Lengthe);
                    Point2d po7 = new Point2d(Length - Lengthg - Lengthf, -Lengthb - Lengthc - Lengthe);
                    Point2d po8 = new Point2d(Length - Lengthg - Lengthf, -Lengthb - Lengthc);
                    Point2d po9 = new Point2d(Length - Lengthg, -Lengthb - Lengthc);
                    Point2d po10 = new Point2d(0, -Lengthb);


                    //Line line1 = new Line(po1, po2);
                    //line1.ColorIndex = 1; // đỏ, 2 xanh, 3 vàng.....
                    //line1.Layer = "layer name"; //kiếm cái layername tồn tại trong cad đáp vào.
                    //line1.LineWeight = LineWeight.LineWeight013;
                    //line1.Thickness = 10;
                    //đấy cái này phải đọc api của cad thôi.hoặc mò z.
                    // từ khóa là: set color for line in autocad by api


                    // Tạo Pline thứ 1
                    Polyline pl = new Polyline();
                    pl.SetDatabaseDefaults();
                    pl.AddVertexAt(0, po1, 0, 0, 0);
                    pl.AddVertexAt(1, po2, 0, 0, 0);
                    pl.AddVertexAt(2, po3, 0, 0, 0);
                    pl.AddVertexAt(3, po4, 0, 0, 0);
                    pl.AddVertexAt(4, po5, 0, 0, 0);
                    pl.AddVertexAt(5, po6, 0, 0, 0);
                    pl.AddVertexAt(6, po7, 0, 0, 0);
                    pl.AddVertexAt(7, po8, 0, 0, 0);
                    pl.AddVertexAt(8, po9, 0, 0, 0);
                    pl.AddVertexAt(9, po10, 0, 0, 0);
                    pl.AddVertexAt(10, po1, 0, 0, 0);

                    pl.Layer = sLayerName;
                    tbrecord.AppendEntity(pl);
                    tran.AddNewlyCreatedDBObject(pl, true);

                    // Tạo Pline thứ 2
                    Polyline pl2 = new Polyline();
                    pl2.SetDatabaseDefaults();
                    pl2.AddVertexAt(0, po10, 0, 0, 0);
                    pl2.AddVertexAt(1, po3, 0, 0, 0);

                    pl2.Layer = sLayerName2;
                    tbrecord.AppendEntity(pl2);
                    tran.AddNewlyCreatedDBObject(pl2, true);


                    // Hatch
                    // Adds the arc and line to an object id collection
                    ObjectIdCollection acObjIdColl = new ObjectIdCollection();
                    acObjIdColl.Add(pl.ObjectId);
                    // Create the hatch object and append it to the block table record
                    Hatch acHatch = new Hatch();
                    tbrecord.AppendEntity(acHatch);
                    tran.AddNewlyCreatedDBObject(acHatch, true);
                    // Set the properties of the hatch object
                    // Associative must be set after the hatch object is appended to the
                    // block table record and before AppendLoop
                    acHatch.SetDatabaseDefaults();
                    acHatch.SetHatchPattern(HatchPatternType.PreDefined, "ANSI31");
                    acHatch.Associative = true;
                    acHatch.AppendLoop(HatchLoopTypes.Outermost, acObjIdColl);
                    // Evaluate the hatch
                    acHatch.EvaluateHatch(true);
                    // Increase the pattern scale by 2 and re-evaluate the hatch
                    acHatch.PatternScale = acHatch.PatternScale + 100;
                    acHatch.SetHatchPattern(acHatch.PatternType, acHatch.PatternName);
                    acHatch.EvaluateHatch(true);

                }




                catch (Autodesk.AutoCAD.Runtime.Exception ex)
                {
                    // ở đấycó lỗi, muốn làm gì thì làm :))
                }



                tran.Commit();
            }

        }

        //Click clearLine thì gọi ClearLine
        public void Move()
        {
            // Get the current document and database
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            // Start a transaction
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Open the Block table for read
                BlockTable acBlkTbl;
                acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId,
                                             OpenMode.ForRead) as BlockTable;

                // Open the Block table record Model space for write
                BlockTableRecord acBlkTblRec;
                acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                                OpenMode.ForWrite) as BlockTableRecord;

                // Create a circle that is at 2,2 with a radius of 0.5
                Circle acCirc = new Circle();
                acCirc.SetDatabaseDefaults();
                acCirc.Center = new Point3d(2, 2, 0);
                acCirc.Radius = 0.5;

                // Create a matrix and move the circle using a vector from (0,0,0) to (2,0,0)
                Point3d acPt3d = new Point3d(0, 0, 0);
                Vector3d acVec3d = acPt3d.GetVectorTo(new Point3d(2, 0, 0));

                acCirc.TransformBy(Matrix3d.Displacement(acVec3d));

                // Add the new object to the block table record and the transaction
                acBlkTblRec.AppendEntity(acCirc);
                acTrans.AddNewlyCreatedDBObject(acCirc, true);

                // Save the new objects to the database
                acTrans.Commit();

            }
        }
        public void moveselect()
        {
            Editor edms = Application.DocumentManager.MdiActiveDocument.Editor;
            Database dbms = HostApplicationServices.WorkingDatabase;

            PromptSelectionOptions sletcop = new PromptSelectionOptions();
            sletcop.AllowDuplicates = false;
            sletcop.MessageForAdding = "người dùng thêm đối tượng";
            sletcop.MessageForRemoval = "người dùng xóa đối tượng";

            TypedValue[] typvl = new TypedValue[4];
            typvl.SetValue(new TypedValue((int)DxfCode.Operator, "<or"), 0);
            typvl.SetValue(new TypedValue((int)DxfCode.Start, "LINE"), 1);
            typvl.SetValue(new TypedValue((int)DxfCode.Start, "TEXT"), 2);
            typvl.SetValue(new TypedValue((int)DxfCode.Operator, "or>"), 3);
            SelectionFilter sltFilter = new SelectionFilter(typvl);

            PromptSelectionResult promptrs = edms.GetSelection(sltFilter);
            SelectionSet acSSet1 = promptrs.Value;

            if (promptrs.Status != PromptStatus.OK);
            
                using(Transaction acTranms = dbms.TransactionManager.StartTransaction())
                {
                    BlockTable acBlkTbl;
                    acBlkTbl = acTranms.GetObject(dbms.BlockTableId,
                                                 OpenMode.ForRead) as BlockTable;

                    BlockTableRecord acBlkTblRecc;
                    acBlkTblRecc = acTranms.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                                    OpenMode.ForWrite) as BlockTableRecord;

                    foreach(SelectedObject selec in acSSet1)
                    {
                        
                        if (acTranms.GetObject(selec.ObjectId, OpenMode.ForWrite) is Line Line)
                        {
                            if( Line != null)
                            {
                                Point3d acPt3d = new Point3d(0, 0, 0);
                                Vector3d acVec3d = acPt3d.GetVectorTo(new Point3d(2000, 0, 0));
                                Line.TransformBy(Matrix3d.Displacement(acVec3d));
                                acBlkTblRecc.AppendEntity(Line);
                                acTranms.AddNewlyCreatedDBObject(Line, true);
                            }    
                            
                        }
                        if (acTranms.GetObject(selec.ObjectId, OpenMode.ForWrite) is  MText polyline)
                        {
                            if (polyline != null)
                        {
                           
                                Point3d acPt3d = new Point3d(0, 0, 0);
                                Vector3d acVec3d = acPt3d.GetVectorTo(new Point3d(2000, 0, 0));
                                polyline.TransformBy(Matrix3d.Displacement(acVec3d));
                                acBlkTblRecc.AppendEntity(polyline);
                                acTranms.AddNewlyCreatedDBObject(polyline, true);
                            }
                            
                        }
                        
                    }
                    // Save the new objects to the database
                    acTranms.Commit();
                
            }
            //else
            //    edms.WriteMessage("\n không chọn được đối tượng");
        }

        //[CommandMethod("MergeSelectionSets")]
        //public static void MergeSelectionSets()
        //{
        //    // Get the current document editor
        //    Editor acDocEd = Application.DocumentManager.MdiActiveDocument.Editor;

        //    // Request for objects to be selected in the drawing area
        //    PromptSelectionResult acSSPrompt;
        //    acSSPrompt = acDocEd.GetSelection();

        //    SelectionSet acSSet1;
        //    ObjectIdCollection acObjIdColl = new ObjectIdCollection();

        //    // If the prompt status is OK, objects were selected
        //    if (acSSPrompt.Status == PromptStatus.OK)
        //    {
        //        // Get the selected objects
        //        acSSet1 = acSSPrompt.Value;

        //        // Append the selected objects to the ObjectIdCollection
        //        acObjIdColl = new ObjectIdCollection(acSSet1.GetObjectIds());
        //    }

        //    // Request for objects to be selected in the drawing area
        //    acSSPrompt = acDocEd.GetSelection();

        //    SelectionSet acSSet2;

        //    // If the prompt status is OK, objects were selected
        //    if (acSSPrompt.Status == PromptStatus.OK)
        //    {
        //        acSSet2 = acSSPrompt.Value;

        //        // Check the size of the ObjectIdCollection, if zero, then initialize it
        //        if (acObjIdColl.Count == 0)
        //        {
        //            acObjIdColl = new ObjectIdCollection(acSSet2.GetObjectIds());
        //        }
        //        else
        //        {
        //            // Step through the second selection set
        //            foreach (ObjectId acObjId in acSSet2.GetObjectIds())
        //            {
        //                // Add each object id to the ObjectIdCollection
        //                acObjIdColl.Add(acObjId);
        //            }
        //        }
        //    }

        //    Application.ShowAlertDialog("Number of objects selected: " +
        //                                acObjIdColl.Count.ToString());
        //}
    }
}
