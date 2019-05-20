using ConflictRenewal.Data;
using ConflictRenewal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KellermanSoftware.CompareNetObjects;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace ConflictRenewal.ViewModel
{
    public class AuditTrail
    {
      //  private readonly ApplicationDbContext _context;
        ApplicationDbContext _context = new ApplicationDbContext();

        public AuditTrail(ApplicationDbContext context)
        {
            _context = context;
        }
        public int Id { get; set; }
        public DateTime ConflictDate { get; set; }
        public string Question1 { get; set; }
        public string Question2 { get; set; }
        public string Question3 { get; set; }
        public string Question4 { get; set; }
        public string Question5 { get; set; }
        public string Question6 { get; set; }
        public DateTime? MostrecentjournalDate { get; set; }
        public string EmailID { get; set; }
        public string AdminRole { get; set; }
        public int ConflictStatus { get; set; }
        public string CreatedBy { get; set; }


        public AuditTrail GetData(int ID)
        {
            AuditTrail mod = new AuditTrail(_context);
            ApplicationDbContext ent = new ApplicationDbContext();
            Conflict rec = ent.Conflict.FirstOrDefault(s => s.Id == ID);
            if (rec != null)
            {
                mod.Id = rec.Id;
                mod.AdminRole = rec.AdminRole;
                mod.ConflictDate = rec.ConflictDate;
                mod.ConflictStatus = rec.ConflictStatus;
                mod.CreatedBy = rec.CreatedBy;
                mod.EmailID = rec.EmailID;
                //mod.Journals = rec.Journals;
                mod.MostrecentjournalDate = rec.MostrecentjournalDate;
                mod.Question1 = rec.Question1;
                mod.Question2 = rec.Question2;
                mod.Question3 = rec.Question3;
                mod.Question4 = rec.Question4;
                mod.Question5 = rec.Question5;
                mod.Question6 = rec.Question6;
            }
            return mod;
        }

        public void DeleteRecord(int ID)
        {
            ApplicationDbContext ent = new ApplicationDbContext();
            Conflict rec = ent.Conflict.FirstOrDefault(s => s.Id == ID);
            if (rec != null)
            {
                Conflict DummyObject = new Conflict(); // Storage of this null object shows data after delete = nix, naught, nothing!
                //rec.Deleted = true;
                ent.SaveChanges();
                CreateAuditTrail(AuditActionType.Delete, ID, rec, DummyObject);
            }
        }

        public void CreateAuditTrail(AuditActionType Action, int KeyFieldID, Object OldObject, Object NewObject)
        {
            // get the differance
            CompareLogic compObjects = new CompareLogic();
            compObjects.Config.MaxDifferences = 99;
            ComparisonResult compResult = compObjects.Compare(OldObject, NewObject);
            List<AuditDelta> DeltaList = new List<AuditDelta>();
            foreach (var change in compResult.Differences)
            {
                AuditDelta delta = new AuditDelta();
                if (change.PropertyName.Substring(0, 1) == ".")
                {
                    delta.FieldName = change.PropertyName.Substring(1, change.PropertyName.Length - 1);
                }
                delta.FieldName = change.PropertyName;
                delta.ValueBefore = change.Object1Value;
                delta.ValueAfter = change.Object2Value;
                DeltaList.Add(delta);
            }
            AuditTrailTable audit = new AuditTrailTable();
            audit.AuditActionTypeENUM = (int)Action;
            audit.DataModel = this.GetType().Name;
            audit.DateTimeStamp = DateTime.Now;
            audit.KeyFieldID = KeyFieldID;
            audit.ValueBefore = JsonConvert.SerializeObject(OldObject); // if use xml instead of json, can use xml annotation to describe field names etc better
            audit.ValueAfter = JsonConvert.SerializeObject(NewObject);
            audit.Changes = JsonConvert.SerializeObject(DeltaList);
            
            _context.AuditTrailTable.Add(audit);
            _context.SaveChanges();

        }

        public List<AuditTrail> GetAllData(bool ShowDeleted)
        {
            List<AuditTrail> rslt = new List<AuditTrail>();
            ApplicationDbContext ent = new ApplicationDbContext();
            List<Conflict> SearchResults = new List<Conflict>();

            //if (ShowDeleted)
                SearchResults = ent.Conflict.ToList();
            //else
            //    SearchResults = ent.Conflict.Where(s => s.Deleted == false).ToList();

            foreach (var record in SearchResults)
            {
                AuditTrail rec = new AuditTrail(_context);
                rec.Id = record.Id;
                rec.ConflictDate = record.ConflictDate;
                rec.ConflictStatus = record.ConflictStatus;
                rec.EmailID = record.EmailID;
                rec.CreatedBy = record.CreatedBy;
               // rec.Journals = record.Journals;
                rec.MostrecentjournalDate = record.MostrecentjournalDate;
                rec.Question1 = record.Question1;
                rec.Question2 = record.Question2;
                rec.Question3 = record.Question3;
                rec.Question4 = record.Question4;
                rec.Question5 = record.Question5;
                rec.Question6 = record.Question6;
                //rec. = record.Journals;
                rslt.Add(rec);
            }
            return rslt;
        }

        public bool UpdateRecord(Conflict Rec)
        {
            bool rslt = false;
            
            var dbRec = _context.Conflict.FirstOrDefault(s => s.Id == Rec.Id);
            if (dbRec != null)
            {
                // audit process 1 - gather old values
                Conflict OldRecord = new Conflict();
                OldRecord.Id = dbRec.Id;
                OldRecord.AdminRole = dbRec.AdminRole;
                OldRecord.MostrecentjournalDate = dbRec.MostrecentjournalDate;
                OldRecord.ConflictDate = dbRec.ConflictDate;
                OldRecord.ConflictStatus = dbRec.ConflictStatus;
                OldRecord.CreatedBy = dbRec.CreatedBy;
                OldRecord.EmailID = dbRec.EmailID;
                OldRecord.Journals = dbRec.Journals;
                OldRecord.MostrecentjournalDate = dbRec.MostrecentjournalDate;
                OldRecord.Question1 = dbRec.Question1;
                OldRecord.Question2 = dbRec.Question2;
                OldRecord.Question3 = dbRec.Question3;
                OldRecord.Question4 = dbRec.Question4;
                OldRecord.Question5 = dbRec.Question5;
                OldRecord.Question6 = dbRec.Question6;
                
                // update the live record
                dbRec.Id = Rec.Id;
                dbRec.AdminRole = Rec.AdminRole;
                dbRec.MostrecentjournalDate = Rec.MostrecentjournalDate;
                dbRec.ConflictDate = Rec.ConflictDate;
                dbRec.ConflictStatus = Rec.ConflictStatus;
                dbRec.CreatedBy = Rec.CreatedBy;
                dbRec.EmailID = Rec.EmailID;
                dbRec.Journals = Rec.Journals;
                dbRec.MostrecentjournalDate = Rec.MostrecentjournalDate;
                dbRec.Question1 = Rec.Question1;
                dbRec.Question2 = Rec.Question2;
                dbRec.Question3 = Rec.Question3;
                dbRec.Question4 = Rec.Question4;
                dbRec.Question5 = Rec.Question5;
                dbRec.Question6 = Rec.Question6;

                _context.SaveChanges();

                CreateAuditTrail(AuditActionType.Update, Rec.Id, OldRecord, Rec);

                rslt = true;
            }
            return rslt;
        }

        public void CreateRecord(Conflict Rec)
        {

           // ApplicationDbContext ent = new ApplicationDbContext();
            Conflict dbRec = new Conflict();
            dbRec.AdminRole = Rec.AdminRole;
            dbRec.ConflictDate = Rec.ConflictDate;
            dbRec.ConflictStatus = Rec.ConflictStatus;
            dbRec.CreatedBy = Rec.CreatedBy;
            dbRec.EmailID = Rec.EmailID;
            dbRec.Journals = Rec.Journals;
            dbRec.MostrecentjournalDate = Rec.MostrecentjournalDate;
            dbRec.Question1 = Rec.Question1;
            dbRec.Question2 = Rec.Question2;
            dbRec.Question3 = Rec.Question3;
            dbRec.Question4 = Rec.Question4;
            dbRec.Question5 = Rec.Question5;
            dbRec.Question6 = Rec.Question6;
            _context.Conflict.Add(dbRec);
            _context.SaveChanges(); // save first so we get back the dbRec.ID for audit tracking
            Conflict DummyObject = new Conflict(); // Storage of this null object shows data before creation = nix, naught, nothing!

            CreateAuditTrail(AuditActionType.Create, dbRec.Id, DummyObject, dbRec);

        }


        public List<AuditChange> GetAudit(int ID)
        {
            List<AuditChange> rslt = new List<AuditChange>();
            var AuditTrail = _context.AuditTrailTable.Where(s => s.KeyFieldID == ID).OrderByDescending(s => s.DateTimeStamp); // we are looking for audit-history of the record selected.
            var serializer = new XmlSerializer(typeof(AuditDelta));
            foreach (var record in AuditTrail)
            {
                AuditChange Change = new AuditChange();
                Change.DateTimeStamp = record.DateTimeStamp.ToString();
                Change.AuditActionType = (AuditActionType)record.AuditActionTypeENUM;
                Change.AuditActionTypeName = Enum.GetName(typeof(AuditActionType), record.AuditActionTypeENUM);
                List<AuditDelta> delta = new List<AuditDelta>();
                delta = JsonConvert.DeserializeObject<List<AuditDelta>>(record.Changes);
                Change.Changes.AddRange(delta);
                rslt.Add(Change);
            }
            return rslt;
        }

    }

    public class AuditChange
    {
        public string DateTimeStamp { get; set; }
        public AuditActionType AuditActionType { get; set; }
        public string AuditActionTypeName { get; set; }
        public List<AuditDelta> Changes { get; set; }
        public AuditChange()
        {
            Changes = new List<AuditDelta>();
        }
    }

    public class AuditDelta
    {
        public string FieldName { get; set; }
        public string ValueBefore { get; set; }
        public string ValueAfter { get; set; }
    }

    public enum AuditActionType
    {
        Create = 1,
        Update,
        Delete
    }


}
