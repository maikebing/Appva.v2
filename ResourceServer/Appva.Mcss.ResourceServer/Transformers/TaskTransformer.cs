// <copyright file="TaskTransformer.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a></author>
namespace Appva.Mcss.ResourceServer.Transformers
{
    #region Imports 

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Domain.Entities;
    using Appva.Mcss.ResourceServer.Models;
    using Appva.Mcss.Utils;

    #endregion

    /// <summary>
    /// Task transforming.
    /// </summary>
    public static class TaskTransformer
    {
        /// <summary>
        /// TODO: Summary.
        /// </summary>
        /// <param name="tasks">TODO: tasks</param>
        /// <param name="time">TODO: time</param>
        /// <param name="stdStatusItems">TODO: stdStatusItems</param>
        /// <param name="user">TODO: user</param>
        /// <returns>TODO: returns</returns>
        public static dynamic ToTaskModel(IList<Task> tasks, DateTime time, IList<Taxon> stdStatusItems, Account user)
        {
            var retval = new Dictionary<string, TimeslotModel>();
            foreach (var task in tasks) 
            {
                var schedule = task.Schedule.ScheduleSettings.Name;
                if (! retval.ContainsKey(schedule)) 
                {
                    var timeslotModel = new TimeslotModel 
                    {
                        Timeslot = string.Format("{0:u}", time),
                        Category = schedule,
                        Tasks = new List<HydratedTaskModel>()
                    };
                    retval.Add(schedule, timeslotModel);
                }
                retval[schedule].Tasks.Add(ToTaskModel(task, stdStatusItems, user));
            }

            return retval.Select(x => x.Value).ToList();
        }

        /// <summary>
        /// TODO: Summary.
        /// </summary>
        /// <param name="task">TODO: task</param>
        /// <param name="stdStatusItems">TODO: stdStatusItems</param>
        /// <param name="user">TODO: user</param>
        /// <returns>TODO: returns</returns>
        public static HydratedTaskModel ToTaskModel(Task task, IList<Taxon> stdStatusItems, Account user)
        {
            List<string> dateTimeInterval = new List<string>();
            if (task.Schedule.ScheduleSettings.ScheduleType == ScheduleType.Calendar)
            {
                dateTimeInterval.Add(string.Format("{0:u}", task.StartDate));
                dateTimeInterval.Add(string.Format("{0:u}", task.EndDate.GetValueOrDefault()));
            }
            else if (task.Schedule.ScheduleSettings.ScheduleType == ScheduleType.Action)
            {
                dateTimeInterval.Add(string.Format("{0:u}", task.Scheduled.AddMinutes(-task.RangeInMinutesBefore)));
                dateTimeInterval.Add(string.Format("{0:u}", task.Scheduled.AddMinutes(task.RangeInMinutesAfter)));
            }
            var contacts = new Dictionary<string, ContactModel>();
            return new HydratedTaskModel
            {
                DateTimeInterval = dateTimeInterval,
                DateTimeScheduled = string.Format("{0:u}", task.Scheduled),
                Description = task.Sequence.Description,
                Id = task.Id,
                SequenceId = task.Sequence.Id,
                Inventory = GetInventory(task), //// TODO: Inventory has static reasons 
                Name = task.Name,
                Permissions = GetPermissions(task, user), 
                Statuses = GetStatuses(task),
                StatusItems = GetStatusItems(task, stdStatusItems, ref contacts),
                Type = new List<string>(), //// FIXME: Task type
                Refill = GetRefillModel(task.Sequence),
                Completed = GetCompletedStatus(task),
                Contacts = contacts //// FIXME: Shall include accounts to contact also
            };
        }

        #region Helpers

        /// <summary>
        /// Returns a list of statusitems for the current task.
        /// </summary>
        /// <param name="task">TODO: task</param>
        /// <param name="stdStatusItems">TODO: stdStatusItems</param>
        /// <param name="contacts">TODO: contacts></param>
        /// <returns>TODO: returns</returns>
        private static List<StatusItemModel> GetStatusItems(Task task, IList<Taxon> stdStatusItems, ref Dictionary<string, ContactModel> contacts)
        {
            var items = task.Schedule.ScheduleSettings.StatusTaxons.Count > 0 ? task.Schedule.ScheduleSettings.StatusTaxons : stdStatusItems;
            if (task.Schedule.ScheduleSettings.NurseConfirmDeviation)
            {
                var retval = new List<StatusItemModel>();
                foreach (var item in items.OrderBy(x => x.Weight).ToList())
                {
                    retval.Add(TaxonTransformer.ToStatusItemModel(item, task.Schedule.ScheduleSettings.Id.ToString()));
                    if (! contacts.ContainsKey(task.Schedule.ScheduleSettings.Id.ToString())) 
                    {
                        contacts.Add(task.Schedule.ScheduleSettings.Id.ToString(), CreateContactModel(task.Schedule.ScheduleSettings));
                    }
                }
                return retval;
            }
            return TaxonTransformer.ToStatusItemModel(items.OrderBy(x => x.Weight).ToList());
        }

        /// <summary>
        /// Creates a model with contact-dialog message for the schedule
        /// FIXME: if scheduleSettings.SpecificNurseConfirm shall Accounts be populated with accounts to contact
        /// </summary>
        /// <param name="scheduleSettings">TODO: scheduleSettings</param>
        /// <returns>TODO: returns</returns>
        private static ContactModel CreateContactModel(ScheduleSettings scheduleSettings)
        {
            var stdMessage = string.Format("<h2>Kontakta sjuksköterska</h2><form method='post' action='#'><div class='text'><p>Du måste kontakta sjuksköterska vid avvikelse.");
            var message = scheduleSettings.NurseConfirmDeviationMessage.IsNotEmpty() ? scheduleSettings.NurseConfirmDeviationMessage : stdMessage; 
            return new ContactModel
            { 
                Title = message.Substring(4, message.IndexOf("</h2>")-4),
                Text = message.Substring(message.IndexOf("<p>")+3)
            };
        }

        /// <summary>
        /// Returns a model with details of the completion of the task if it is completed
        /// </summary>
        /// <param name="task">TODO: task</param>
        /// <returns>TODO: returns</returns>
        private static CompletedDetailsModel GetCompletedStatus(Task task)
        {
            if (task.IsCompleted)
            {
                return new CompletedDetailsModel
                {
                    Accounts = new List<string> { task.CompletedBy.FullName },
                    Time = task.CompletedDate.GetValueOrDefault(),
                    Status = TaxonTransformer.ToStatusItemModel(task.StatusTaxon)
                };
            }
            return null;
        }

        /// <summary>
        /// Gets the current statuses of the task. Eg if the task is completed or delayed
        /// </summary>
        /// <param name="task">TODO: task</param>
        /// <returns>TODO: returns</returns>
        private static List<string> GetStatuses(Task task)
        {
            var retval = new List<string>();
            if (task.Delayed)
            {
                retval.Add("delayed");
            }
            if (task.IsCompleted)
            {
                retval.Add("completed");
            }
            return retval;
        }

        /// <summary>
        /// Checks which permissions the current user has to the current task.
        /// Should primary check if the user has needed status (title) and needed delegation 
        /// to completet the task. 
        /// </summary>
        /// <param name="task">TODO: task</param>
        /// <param name="user">TODO: user</param>
        /// <returns>TODO: returns</returns>
        private static List<string> GetPermissions(Task task, Account user)
        {
            var retval = new List<string>();
            if (task.Schedule.ScheduleSettings.ScheduleType == ScheduleType.Calendar)
            {
                if (! task.Sequence.CanRaiseAlert)
                {
                    return retval;
                }
            }
            //// Nurse should be able to sign everything
            if (user.IsInRole("_TITLE_NURSE"))
            {
                retval.Add("complete");
            }
            else 
            {
                //// If task not need to be signed by a nurse (Should be a check in future to see if user have the required role to sign something but today it can only be nurse-role)
                if (task.Sequence.Role.IsNull())
                {
                    //// If task requires delegation
                    if (task.Sequence.Taxon.IsNotNull())
                    {
                        foreach (var delegation in user.Delegations)
                        {
                            //// Check if the delegation is guilty (Active, not pending, have started and not ended
                            if (delegation.Active && !delegation.Pending && delegation.StartDate <= DateTime.Now && delegation.EndDate >= DateTime.Now)
                            {
                                //// Check if delegation is the same as needed for task
                                if (delegation.Taxon.Equals(task.Taxon))
                                {
                                    //// Check if delegation is guilty for current patient
                                    if (delegation.IsGlobal || delegation.Patients.Contains(task.Patient))
                                    {
                                        retval.Add("complete");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        retval.Add("complete");
                    }
                }
            }
            return retval;
        }

        /// <summary>
        /// Creates a refill model if Refill is active both for tenant and for current schedule
        /// </summary>
        /// <param name="sequence">TODO: sequence</param>
        /// <returns>TODO: returns</returns>
        private static RefillModel GetRefillModel(Sequence sequence)
        {
            if (sequence.Schedule.ScheduleSettings.OrderRefill && true /* Should check tenant-settings */) 
            {
                return new RefillModel
                {
                    Id = sequence.Id,
                    Ordered = sequence.Refill,
                    RefillOrderedBy = sequence.Refill ? sequence.RefillOrderedBy.FullName : null,
                    RefillOrderedTime = sequence.Refill ? string.Format("{0:u}", sequence.RefillOrderedDate) : null
                };    
            }
            return null;
        }

        /// <summary>
        /// Gets the inventory for current task.
        /// FIXME: Should be defineable statuses and amounts.
        /// </summary>
        /// <param name="task">TODO: task</param>
        /// <returns>TODO: returns</returns>
        private static InventoryModel GetInventory(Task task)
        {
            if (task.Schedule.ScheduleSettings.HasInventory)
            {
                return new InventoryModel
                { 
                    Id = task.Sequence.Inventory.Id, 
                    Value = task.Sequence.Inventory.CurrentLevel,
                    Reasons = new List<string>
                    {
                        "Medicinering",
                        "Tidigare uttag oanvändbart",
                        "Dosettdelning",
                        "Kassering",
                        "Delning hos anna patient"
                    },
                    Amounts = new List<double> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }
                };
            }
            return null;
        }
        #endregion
    }
}