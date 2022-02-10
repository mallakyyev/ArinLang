using System;
using System.Collections.Generic;
using System.Text;

namespace TSTB.DAL.Models.Enums
{
    public enum StatusPayment
    {
        OrderRegisteredButNotPaid = 0,
        PreauthorizedAmountFrozen = 1,
        CompleteAuthorizationOrderAmount = 2,
        AuthorizationCanceled = 3,
        RefundOperationPerformedTransaction = 4,
        AuthorizationInitiatedIssuingBankACS = 5, 
        AuthorizationRejected = 6,
        NotPaid = 99
    }
}
