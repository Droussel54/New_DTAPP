﻿using New_DTAPP.Data;
using New_DTAPP.Models;

namespace New_DTAPP.Extensions
{
    public static class EntityToModel
    {
        #region File
        public static FileModel? EntityToModelFile(this Data.File item)
        {
            if (item == null) return null;

            FileModel obj = new FileModel()
            {
                FileId = item.FileId,
                FileName = item.FileName,
                FileSize = item.FileSize,
                TransferId = item.TransferId,
                FileSent = item.FileSent,
                FileComment = item.Comment!,
                FileExt = item.FileExt,
            };

            return obj;
        }

        public static List<FileModel>? EntityToModelFile(this ICollection<Data.File> items)
        {
            if (items == null) return null;

            List<FileModel> obj = new List<FileModel>();

            foreach (var x in items)
            {
                obj.Add(x.EntityToModelFile()!);
            }

            return obj;
        }
        #endregion

        #region Operation
        public static OperationModel? EntityToModelOperation(this Operation item)
        {
            if (item == null) return null;

            OperationModel obj = new OperationModel()
            {
                OperationId = item.OperationId,
                OperationName = item.OperationName,
                Archived = item.Archived,
            };

            return obj;
        }

        public static List<OperationModel>? EntityToModelOperation(this ICollection<Data.Operation> items)
        {
            if (items == null) return null;

            List<OperationModel> obj = new List<OperationModel>();

            foreach (var x in items)
            {
                obj.Add(x.EntityToModelOperation()!);
            }

            return obj;
        }

        #endregion

        #region Role 
        public static RoleModel? EntityToModelRole(this Data.Role item)
        {
            if (item == null) return null;

            RoleModel obj = new RoleModel()
            {
                RoleId = item.RoleId,
                RoleName = item.RoleName,
                Ordering = item.Ordering,
            };

            return obj;
        }

        public static List<RoleModel>? EntityToModelRole(this ICollection<Data.Role> items)
        {
            if (items == null) return null;

            List<RoleModel> obj = new List<RoleModel>();

            foreach (var x in items)
            {
                obj.Add(x.EntityToModelRole()!);
            }

            return obj;
        }
        #endregion

        #region System 
        public static SystemModel? EntityToModelSystem(this Data.System item)
        {
            if (item == null) return null;

            SystemModel obj = new SystemModel()
            {
                SystemId = item.SystemId,
                SystemName = item.SystemName,
                Archived = item.Archived,
            };

            return obj;
        }

        public static List<SystemModel>? EntityToModelSystem(this ICollection<Data.System> items)
        {
            if (items == null) return null;

            List<SystemModel> obj = new List<SystemModel>();

            foreach (var x in items)
            {
                obj.Add(x.EntityToModelSystem()!);
            }

            return obj;
        }
        #endregion

        #region Transfer
        public static TransferModel? EntityToModelTransfer(this Transfer item)
        {
            if (item == null) return null;

            TransferModel obj = new TransferModel()
            {
                TransferId = item.TransferId,
                RequestCreatedAt = item.RequestCreatedAt,
                SentTime = item.SentTime,
                ClientName = item.ClientName,
                ClientUnitId = item.ClientUnitId,
                ClientUnit = item.ClientUnit == null ? null : item.ClientUnit.EntityToModelUnit(),
                OperationId = item.OperationId,
                Operation = item.Operation == null ? null : item.Operation.EntityToModelOperation(),
                OrigSystemId = item.OrigSystemId,
                OrigSystem = item.OrigSystem.EntityToModelSystem()!,
                DestSystemId = item.DestSystemId,
                DestSystem = item.DestSystem.EntityToModelSystem()!,
                IssoApproval = item.IssoApproval,
                IssueReported = item.IssueReported,
                SpillPrevented = item.SpillPrevented,
                SpillOccurred = item.SpillOccurred,
                SpillId = item.SpillId,
                Spill = item.Spill == null ? null : item.Spill.EntityToModelSpill(),
                Comments = item.Comments,
                Urgent = item.Urgent,
                Reviewed = item.Reviewed,
                ReviewedUserId = item.ReviewedUserId,
                ReviewedUser = item.ReviewedUser == null ? null : item.ReviewedUser.EntityToModelUser(),
                CompletedUserId = item.CompletedUserId,
                CompletedUser = item.CompletedUser == null ? null : item.CompletedUser.EntityToModelUser(),
                CompletedAt = item.CompletedAt,
                Completed = item.Completed,
                Files = item.Files.EntityToModelFile()!,
                TransferTypeId = item.TransferTypeId,
                TransferTypeModel = item.TransferType.EntityToModelTransferType(),
            };

            return obj;
        }

        public static List<TransferModel>? EntityToModelTransfer(this ICollection<Data.Transfer> items)
        {
            if (items == null) return null;

            List<TransferModel> obj = new List<TransferModel>();

            foreach (var x in items)
            {
                obj.Add(x.EntityToModelTransfer()!);
            }

            return obj;
        }
        #endregion

        #region Unit
        public static Models.UnitModel? EntityToModelUnit(this Data.Unit item)
        {
            if (item == null) return null;

            Models.UnitModel obj = new Models.UnitModel()
            {
                UnitId = item.UnitId,
                UnitName = item.UnitName,
                Archived = item.Archived,
            };

            return obj;
        }

        public static List<Models.UnitModel>? EntityToModelUnit(this List<Data.Unit> items)
        {
            if (items == null) return null;

            List<Models.UnitModel> obj = new List<Models.UnitModel>();

            foreach (var x in items)
            {
                obj.Add(x.EntityToModelUnit()!);
            }

            return obj;
        }

        #endregion

        #region User
        public static UserModel? EntityToModelUser(this User item)
        {
            if (item == null) return null;

            UserModel obj = new UserModel()
            {
                UserId = item.UserId,
                Username = item.Username,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Title = item.Title,
                Email = item.Email,
                Disabled = item.Disabled,
                RoleId = item.RoleId,
                Role = item.Role.EntityToModelRole(),
            };

            return obj;
        }

        

        public static List<UserModel>? EntityToModelUser(this ICollection<Data.User> items)
        {
            if (items == null) return null;

            List<UserModel> obj = new List<UserModel>();

            foreach (var x in items)
            {
                obj.Add(x.EntityToModelUser()!);
            }

            return obj;
        }
        #endregion
        
        #region TransferType

        public static TransferTypeModel? EntityToModelTransferType(this TransferType item)
        {
            if (item == null) return null;
            TransferTypeModel obj = new TransferTypeModel()
            {
                TransferTypeId = item.TransferTypeId,
                TransferTypeDesc = item.TransferTypeDesc,
                Archived = item.Archived,
                Ordering = item.Ordering,
            };
            return obj;
        }

        public static List<TransferTypeModel>? EntityToModelTransferType(this ICollection<Data.TransferType> items)
        {
            if (items == null) return null;

            List<TransferTypeModel> obj = new List<TransferTypeModel>();

            foreach (var x in items)
            {
                obj.Add(x.EntityToModelTransferType()!);
            }

            return obj;
        }
        #endregion

        #region FileExtension 
        public static FileExtensionModel? EntityToModelFileExtension(this Data.FileExtension item)
        {
            if (item == null) return null;

            FileExtensionModel obj = new FileExtensionModel()
            {
                FileExtensionId = item.FileExtensionId,
                FileExtensionName = item.FileExtensionName,
                Archived = item.Archived,
            };

            return obj;
        }

        public static List<FileExtensionModel>? EntityToModelFileExtension(this ICollection<Data.FileExtension> items)
        {
            if (items == null) return null;

            List<FileExtensionModel> obj = new List<FileExtensionModel>();

            foreach (var x in items)
            {
                obj.Add(x.EntityToModelFileExtension()!);
            }

            return obj;
        }
        #endregion

        #region Spill
        public static SpillModel? EntityToModelSpill(this Spill item)
        {
            if (item == null) return null;

            SpillModel obj = new SpillModel()
            {
                SpillId = item.SpillId,
                CFNOCIncidentNumber = item.CFNOCIncidentNumber,
                DGDSSIMIncidentNumber = item.DGDSSIMIncidentNumber,
                BurnedAndAnnotated = item.BurnedAndAnnotated,
                IssoInformed = item.IssoInformed,
                ManagerInformed = item.ManagerInformed,
                NatureOfSpill = item.NatureOfSpill,
                TransferRequestCompleted = item.TransferRequestCompleted,
                EmailTripleDeleted = item.EmailTripleDeleted,
                ClientInformed = item.ClientInformed,
                ConsiderationPowerDown = item.ConsiderationPowerDown,
                CDSent = item.CDSent,
                DateOfSpill = item.DateOfSpill,
                TimeOfSpill = item.TimeOfSpill,
                TimeIdentifiedSpill = item.TimeIdentifiedSpill,
                TimeReported = item.TimeReported,
                WorkstationAffected = item.WorkstationAffected,
                WorkstationAssetNumber = item.WorkstationAssetNumber,
                SpecialistId = item.SpecialistId,
                SpecialistUser = item.SpecialistUser.EntityToModelUser(),
                ReviewerId = item.ReviewerId,
                ReviewerUser = item.ReviewerUser.EntityToModelUser(),
                OrigSystemId = item.OrigSystemId,
                OrigSystem = item.OrigSystem.EntityToModelSystem(),
                DestSystemId = item.DestSystemId,
                DestSystem = item.DestSystem.EntityToModelSystem(),
                TransferId = item.TransferId,
                Transfer = item.Transfer.EntityToModelTransfer(),
                SpillStatusId = item.SpillStatusId,
                SpillStatus = item.SpillStatus.EntityToModelSpillStatus()
            };

            return obj;
        }

        public static List<SpillModel>? EntityToModelSpill(this ICollection<Spill> items)
        {
            if (items == null) return null;

            List<SpillModel> obj = new List<SpillModel>();

            foreach (var x in items)
            {
                obj.Add(x.EntityToModelSpill()!);
            }

            return obj;
        }
        #endregion

        #region SpillStatus
        public static SpillStatusModel? EntityToModelSpillStatus(this SpillStatus item)
        {
            if (item == null) return null;

            SpillStatusModel obj = new SpillStatusModel()
            {
                SpillStatusId = item.SpillStatusId,
                SpillStatusDesc = item.SpillStatusDesc,
                Archived = item.Archived,
            };

            return obj;
        }

        public static List<SpillStatusModel>? EntityToModelSpillStatus(this ICollection<SpillStatus> items)
        {
            if (items == null) return null;

            List<SpillStatusModel> obj = new List<SpillStatusModel>();

            foreach (var x in items)
            {
                obj.Add(x.EntityToModelSpillStatus()!);
            }

            return obj;
        }
        #endregion
    }
}
