using New_DTAPP.Data;
using New_DTAPP.Models;

namespace New_DTAPP.Extensions
{
    public static class ModelToEntity
    {
        #region File
        public static Data.File? ModelToEntityFile(this FileModel item)
        {
            if (item == null) return null;

            Data.File obj = new Data.File
            {
                FileId = item.FileId,
                FileName = item.FileName,
                FileSize = item.FileSize,
                FileExt = item.FileExt,
                TransferId = item.TransferId,
                Transfer = item.Transfer == null ? null : item.Transfer.ModelToEntityTransfer(),
            };

            return obj;
        }

        public static List<Data.File>? ModelToEntityFile(this ICollection<FileModel> items)
        {
            if (items == null) return null;

            List<Data.File> obj = new List<Data.File>();

            foreach (var x in items)
            {
                obj.Add(x.ModelToEntityFile()!);
            }

            return obj;
        }
        #endregion

        #region Operation
        public static Operation? ModelToEntityOperation(this OperationModel item)
        {
            if (item == null) return null;

            Operation obj = new Operation
            {
                OperationId = item.OperationId,
                OperationName = item.OperationName,
                Archived = item.Archived,
            };

            return obj;
        }

        public static List<Operation>? ModelToEntityOperation(this ICollection<OperationModel> items)
        {
            if (items == null) return null;

            List<Operation> obj = new List<Operation>();

            foreach (var x in items)
            {
                obj.Add(x.ModelToEntityOperation()!);
            }

            return obj;
        }

        #endregion

        #region Role 
        public static Role? ModelToEntityRole(this RoleModel item)
        {
            if (item == null) return null;

            Role obj = new Role
            {
                RoleId = item.RoleId,
                RoleName = item.RoleName,
                Ordering = item.Ordering,
            };

            return obj;
        }

        public static List<Role>? ModelToEntityRole(this ICollection<RoleModel> items)
        {
            if (items == null) return null;

            List<Role> obj = new List<Role>();

            foreach (var x in items)
            {
                obj.Add(x.ModelToEntityRole()!);
            }

            return obj;
        }
        #endregion

        #region System 
        public static Data.System? ModelToEntitySystem(this SystemModel item)
        {
            if (item == null) return null;

            Data.System obj = new Data.System
            {
                SystemId = item.SystemId,
                SystemName = item.SystemName,
                Archived = item.Archived,
            };

            return obj;
        }

        public static List<Data.System>? ModelToEntitySystem(this ICollection<SystemModel> items)
        {
            if (items == null) return null;

            List<Data.System> obj = new List<Data.System>();

            foreach (var x in items)
            {
                obj.Add(x.ModelToEntitySystem()!);
            }

            return obj;
        }
        #endregion

        #region TransferType
        public static Data.TransferType? ModelToEntityTransferType(this TransferTypeModel item)
        {
            if (item == null) return null;

            Data.TransferType obj = new Data.TransferType
            {
                TransferTypeId = item.TransferTypeId,
                TransferTypeDesc = item.TransferTypeDesc,
                Archived = item.Archived,
                Ordering = item.Ordering,
            };
            return obj;
        }

        public static List<Data.TransferType>? ModelToEntityTransferType(this ICollection<TransferTypeModel> items)
        {
            if (items == null) return null;

            List<Data.TransferType> obj = new List<Data.TransferType>();

            foreach (var x in items)
            {
                obj.Add(x.ModelToEntityTransferType()!);
            }

            return obj;
        }
        #endregion

        #region Transfer
        public static Transfer? ModelToEntityTransfer(this TransferModel item)
        {
            if (item == null) return null;

            Transfer? obj = new Transfer
            {
                TransferId = item.TransferId,
                RequestCreatedAt = item.RequestCreatedAt,
                SentTime = item.SentTime,
                ClientName = item.ClientName == null ? "" : item.ClientName,
                ClientUnitId = item.ClientUnitId,
                ClientUnit = item.ClientUnit == null ? null : item.ClientUnit.ModelToEntityUnit(),
                OperationId = item.OperationId,
                Operation = item.Operation == null ? null : item.Operation.ModelToEntityOperation(),
                OrigSystemId = item.OrigSystemId,
                OrigSystem = item.OrigSystem.ModelToEntitySystem()!,
                DestSystemId = item.DestSystemId,
                DestSystem = item.DestSystem.ModelToEntitySystem()!,
                IssoApproval = item.IssoApproval,
                IssueReported = item.IssueReported,
                SpillPrevented = item.SpillPrevented,
                SpillOccurred = item.SpillOccurred,
                Comments = item.Comments,
                Urgent = item.Urgent,
                Reviewed = item.Reviewed,
                ReviewedUserId = item.ReviewedUserId,
                ReviewedUser = item.ReviewedUser == null ? null : item.ReviewedUser.ModelToEntityUser(),
                CompletedUserId = item.CompletedUserId,
                CompletedUser = item.CompletedUser == null ? null : item.CompletedUser.ModelToEntityUser(),
                CompletedAt = item.CompletedAt,
                Completed = item.Completed,
                TransferTypeId = item.TransferTypeId,
                TransferType = item.TransferTypeModel.ModelToEntityTransferType(), 
                //Files = item.Files.ModelToEntityFile()!,
            };

            return obj;
        }

        public static List<Transfer>? ModelToEntityTransfer(this ICollection<TransferModel> items)
        {
            if (items == null) return null;

            List<Transfer> obj = new List<Transfer>();

            foreach (var x in items)
            {
                obj.Add(x.ModelToEntityTransfer()!);
            }

            return obj;
        }
        #endregion

        #region Unit
        public static Data.Unit? ModelToEntityUnit(this Models.UnitModel item)
        {
            if (item == null) return null;

            Data.Unit obj = new Data.Unit()
            {
                UnitId = item.UnitId,
                UnitName = item.UnitName,
                Archived = item.Archived,
            };

            return obj;
        }

        public static List<Data.Unit>? ModelToEntityUnit(this ICollection<Models.UnitModel> items)
        {
            if (items == null) return null;

            List<Data.Unit> obj = new List<Data.Unit>();

            foreach (var x in items)
            {
                obj.Add(x.ModelToEntityUnit()!);
            }

            return obj;
        }

        #endregion

        #region User
        public static User? ModelToEntityUser(this UserModel item)
        {
            if (item == null) return null;

            User obj = new User
            {
                UserId = item.UserId,
                Username = item.Username,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Title = item.Title,
                Email = item.Email,
                Disabled = item.Disabled,
                RoleId = item.RoleId,
            };

            return obj;
        }

        public static List<User>? ModelToEntityUser(this ICollection<UserModel> items)
        {
            if (items == null) return null;

            List<User> obj = new List<User>();

            foreach (var x in items)
            {
                obj.Add(x.ModelToEntityUser()!);
            }

            return obj;
        }
        #endregion
    }
}
