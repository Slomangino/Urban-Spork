using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanSpork.Common
{
    public enum Permissions
    {
        #region Access

            #region Building Permissions
            Frontdoor,
            Backdoor,
            CeoOffice,
            AccountingOffice,
            DevelopmentFloor,
            SalesFloor,
            HumanResourcesOffice,
            CustodialCloset,
            MaintenanceCloset,
            ConferenceRoom,
            ExecutiveConferenceRoom,
            #endregion


            #region System Permissions
            VisualStudioProfessional,
            VisualStudioEnterprise,
            IntelliJSuite,
            Windows10,
            MacOsx,
            #endregion

        #endregion


        #region Equipment
        Laptop,
        Desktop,
        Keyboard,
        Mouse,
        Phone
        #endregion
    }
}
