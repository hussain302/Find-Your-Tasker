using Models.DbModels;
using Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mappers.TaskMappers
{
    public static class TaskerMapper
    {
        public static TaskStatusModel ToModel(this Models.DbModels.TaskStatus source)
        {
            return new TaskStatusModel
            {
                TaskStatusId = source.TaskStatusId,
                TaskStatusName = source.TaskStatusName,
                CreatedBy = source.CreatedBy,
                CreatedOn = source.CreatedOn,
                UpdatedBy = source.UpdatedBy,
                UpdatedOn = source.UpdatedOn,
            };
        }

        public static Models.DbModels.TaskStatus ToDb(this TaskStatusModel source)
        {
            return new Models.DbModels.TaskStatus
            {
                TaskStatusId = source.TaskStatusId,
                TaskStatusName = source.TaskStatusName,
                CreatedBy = source.CreatedBy,
                CreatedOn = source.CreatedOn,
                UpdatedBy = source.UpdatedBy,
                UpdatedOn = source.UpdatedOn,
            };
        }
    }
}
