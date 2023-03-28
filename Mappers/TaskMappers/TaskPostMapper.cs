using Mappers.UserMappers;
using Models.DbModels;
using Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mappers.TaskMappers
{
    public static class TaskPostMapper
    {

        public static TasksModel ToModel(this Tasks source)
        {
            return new TasksModel
            {
                Address = source.Address,
                Budget = source.Budget,
                DateOfAssiging = source.DateOfAssiging,
                DueDate=source.DueDate,
                IsValid = source.IsValid,   
                PostedBy = (source.PostedBy != null)? source.PostedBy.ToModel() : null,
                PostedById = source.PostedById,
                RejectionReason = source.RejectionReason,
                TaskDetails = source.TaskDetails,
                Tasker = (source.Tasker != null) ? source.Tasker.ToModel() : null,
                TaskerId = source.TaskerId,
                TaskId = source.TaskId,
                SubCategory = source.SubCategory.ToModel(),
                SubCategoryId = source.SubCategoryId,
                TaskStatus = source.TaskStatus.ToModel(),
                TaskStatusId = source.TaskStatusId,
                TaskTitle = source.TaskTitle,
                CreatedBy = source.CreatedBy,
                CreatedOn = source.CreatedOn,
                UpdatedBy = source.UpdatedBy,
                UpdatedOn = source.UpdatedOn,
            };
        }

        public static Tasks ToDb(this TasksModel source)
        {
            return new Tasks
            {
                Address = source.Address,
                Budget = source.Budget,
                DateOfAssiging = source.DateOfAssiging,
                DueDate = source.DueDate,
                IsValid = source.IsValid,
               // PostedBy = source.PostedBy.ToDb(),
                PostedById = source.PostedById,
                RejectionReason = source.RejectionReason,
                TaskDetails = source.TaskDetails,
                //Tasker = source.Tasker.ToDb(),
                TaskerId = source.TaskerId,
                TaskId = source.TaskId,
                SubCategoryId = source.SubCategoryId,
                //TaskStatus = source.TaskStatus.ToDb(),
                TaskStatusId = source.TaskStatusId,
                TaskTitle = source.TaskTitle,
                CreatedBy = source.CreatedBy,
                CreatedOn = source.CreatedOn,
                UpdatedBy = source.UpdatedBy,
                UpdatedOn = source.UpdatedOn,
            };
        }


    }
}
