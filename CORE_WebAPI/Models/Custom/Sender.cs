using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Sender
    {
        public void UpdateChangedFields(Sender sender)
        {
            if (sender.SenderName != null)
            {
                this.SenderName = sender.SenderName;
            }

            if (sender.SenderSurname != null)
            {
                this.SenderSurname = sender.SenderSurname;
            }

            if (sender.SenderNationalId != null)
            {
                this.SenderNationalId = sender.SenderNationalId;
            }

            if (sender.SenderPassportNo != null)
            {
                this.SenderPassportNo = sender.SenderPassportNo;
            }

            if (sender.SenderEmail != null)
            {
                this.SenderEmail = sender.SenderEmail;
            }

            if (sender.SenderActive != null)
            {
                this.SenderActive = sender.SenderActive;
            }

            if (sender.LoginId != 0)
            {
                this.LoginId = sender.LoginId;
            }
        }
    }
}
