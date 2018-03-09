#region Copyright Koninklijke Philips Electronics N.V. 2014
//
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
// FILENAME: CIPDTO.cs
//
#endregion

using System.Reflection;
using ProtoBuf;

namespace ProtocolBuf12.ECViewModel
{
    /// <summary>
    /// Details of Contrast injection protocol data transfer object.
    /// </summary>
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CipDTO : DTOBase
    {
        /// <summary>
        /// Contrast injection Agent
        /// </summary>
        private string agent = "SRT.G-D101.Intravenous route";
        /// <summary>
        /// Route of administration of contrast injection 
        /// </summary>
        private string routeOfAdministration = "SRT.C-17800.Gadolinium";
        /// <summary>
        /// Volume of dosage
        /// </summary>
        private double volume = 0.0f;
        /// <summary>
        /// Concentration of dosage
        /// </summary>
        private double concentration = 0.5f;
        /// <summary>
        /// Contrast injection starting dynamic number
        /// </summary>
        private uint injectionStartDynamicNumber = 1;
        /// <summary>
        /// Contrast to be ignored or not.
        /// </summary>
        private bool ignore;
        /// <summary>
        /// Raises if the contrast is manual or not.
        /// </summary>
        private bool manual;
        /// <summary>
        /// Notifies if the contrast is confirmed or not.
        /// </summary>
        private bool confirmed;
        /// <summary>
        /// Contrast injection agent
        /// </summary>
        [ViewModelToModel("ExecutionStep.ContrastInjectionProtocol","Agent")]
        public string Agent
        {
            get
            {
                return agent;
            }
            set
            {
                if (agent != value)
                {
                    agent = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Route of administration of contrast injection 
        /// </summary>
        [ViewModelToModel("ExecutionStep.ContrastInjectionProtocol", "RouteOfAdministration")]
        public string RouteOfAdministration
        {
            get
            {
                return routeOfAdministration;
            }
            set
            {
                if (value != routeOfAdministration)
                {
                    routeOfAdministration = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Volume of dosage
        /// </summary>
        [ViewModelToModel("ExecutionStep.ContrastInjectionProtocol", "Volume")]
        public double Volume
        {
            get
            {
                return volume;
            }
            set
            {
                if (volume != value)
                {
                    volume = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Concentration of contrast injection
        /// </summary>
        [ViewModelToModel("ExecutionStep.ContrastInjectionProtocol", "Concentration")]
        public double Concentration
        {
            get
            {
                return concentration;
            }
            set
            {
                if (concentration != value)
                {
                    concentration = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Contrast injection start dynamic number
        /// </summary>
        [ViewModelToModel("ExecutionStep.ContrastInjectionProtocol", "InjectionStartDynamicNumber")]
        public uint InjectionStartDynamicNumber
        {
            get
            {
                return injectionStartDynamicNumber;
            }
            set
            {
                if (injectionStartDynamicNumber != value)
                {
                    injectionStartDynamicNumber = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Should the contrast injection be ignored or not
        /// </summary>
        [ViewModelToModel("ExecutionStep.ContrastInjectionProtocol", "Ignore")]
        public bool Ignore
        {
            get
            {
                return ignore;
            }
            set
            {
                if (ignore != value)
                {
                    ignore = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// is the contrast confirmed or not
        /// </summary>
        [ViewModelToModel("ExecutionStep.ContrastInjectionProtocol", "Confirmed")]
        public bool Confirmed
        {
            get
            {
                return confirmed;
            }
            set
            {
                if (confirmed != value)
                {
                    confirmed = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Is the contrast injection step a manual step
        /// </summary>
        [ViewModelToModel("ExecutionStep.ContrastInjectionProtocol", "Manual")]
        public bool Manual
        {
            get
            {
                return manual;
            }
            set
            {
                if (manual != value)
                {
                    manual = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
    }
}

#region Revision History
// 12-May-2014  Anand K R
//              Initial version
// 2014-Jun-22  Chandralatha
//              Defined mapping between ExecutionStep.ControlInfectionProtocol and CIPDTO
#endregion Revision History
