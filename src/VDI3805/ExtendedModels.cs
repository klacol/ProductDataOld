using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDI3805
{
    public class HeatGenerator  //VDI 3805 Part 3
    {
        public bool IsValid = true;

        public enum ModelOfHeatExchangerEnum { StandardKessel, NiedertemperaturKessel, BrennwertKessel, StandardKombiKessel, NiedertemperaturKombiKessel };
        public ModelOfHeatExchangerEnum ModelOfHeatExchanger { get; set; }

        public List<Property> Properties;

        public HeatGenerator() { }

        public HeatGenerator(RecordSet record, string countryCode, string Vdi2552Code)
        {

            // The following list is copied from VDI 3805 , Part 3, Heat Generators
            // Do not add new attributes to the list, please follow the standard
            // The Copyright is at VDI Verein Deutscher Ingeniere
            // Please purchase a copy of the VDI 3805 before using this code in your application

            Properties = new List<Property>();

            foreach (string prop in record.Fields)
            {
                Properties.Add(new Property(1, "RecordType", record.Fields[1], "", "A3", "700", null));
                Properties.Add(new Property(2, "Index", record.Fields[2], "", "N", "1 to 99999", null));
                Properties.Add(new Property(3, "SortNumberDisplaySequence", record.Fields[3], "", "N", "", null));
                Properties.Add(new Property(4, "ProductRange", record.Fields[4], "", "A", "", null));
                Properties.Add(new Property(5, "ProductName", record.Fields[5], "", "A", "", null));
                Properties.Add(new Property(6, "NominalPower", record.Fields[6], "kW", "N", "", null));
                Properties.Add(new Property(7, "TotalNetMass", record.Fields[7], "kg", "N", "Of heat generator", null));
                Properties.Add(new Property(8, "WaterContent", record.Fields[8], "liter", "N", "Of heat generator", null));
                Properties.Add(new Property(9, "GasContent", record.Fields[9], "liter", "N", "", null));
                Properties.Add(new Property(10, "ThermalLoadLevel3", record.Fields[10], "kW", "N", "", null));
                Properties.Add(new Property(11, "ThermalLoadLevel2", record.Fields[11], "kW", "N", "", null));
                Properties.Add(new Property(12, "ThermalLoadLevel1", record.Fields[12], "kW", "N", "Min. load", null));
                Properties.Add(new Property(13, "LowerModulationLimit", record.Fields[13], "kW", "N", "", null));

                List<EnumValue> enumDesignationForModelOfHeatExchanger = new List<EnumValue>();
                enumDesignationForModelOfHeatExchanger.Add(new EnumValue("1", "Standard-Kessel", ""));
                enumDesignationForModelOfHeatExchanger.Add(new EnumValue("2", "Niedertemperatur-Kessel", ""));
                enumDesignationForModelOfHeatExchanger.Add(new EnumValue("3", "Brennwert-Kessel", ""));
                enumDesignationForModelOfHeatExchanger.Add(new EnumValue("4", "Standard-Kombi-Kessel", ""));
                enumDesignationForModelOfHeatExchanger.Add(new EnumValue("5", "Niedertemperatur-Kombi-Kessel", ""));
                enumDesignationForModelOfHeatExchanger.Add(new EnumValue("6", "Brennwert-Kombi-Kessel", ""));
                enumDesignationForModelOfHeatExchanger.Add(new EnumValue("7", "Durchlauferhitzer für TW-Bereitung", ""));
                enumDesignationForModelOfHeatExchanger.Add(new EnumValue("8", "Trinkwasser-Speicher", ""));
                enumDesignationForModelOfHeatExchanger.Add(new EnumValue("9", "Niederdruck-Dampferzeuger(NDD)", ""));
                enumDesignationForModelOfHeatExchanger.Add(new EnumValue("10", "Hochdruck-Dampferzeuger(HDD)", ""));
                enumDesignationForModelOfHeatExchanger.Add(new EnumValue("11", "Hochdruck-Heißwasser - Kessel", ""));
                enumDesignationForModelOfHeatExchanger.Add(new EnumValue("999", "Sonstige", ""));

                Properties.Add(new Property(14, "CodeNumberDesignationForModelOfHeatExchanger", record.Fields[14], "", "N", "Code number", enumDesignationForModelOfHeatExchanger.Select(x => x.Code).ToList()));
                switch (countryCode)
                {
                    case "DE":
                        Properties.Add(new Property(15, "DesignationForModelOfHeatExchanger", record.Fields[15], "", "A", "", enumDesignationForModelOfHeatExchanger.Select(x => x.ValueDe).ToList()));
                        break;
                    case "EN":
                        Properties.Add(new Property(15, "DesignationForModelOfHeatExchanger", record.Fields[15], "", "A", "", enumDesignationForModelOfHeatExchanger.Select(x => x.ValueEn).ToList()));
                        break;
                }

                List<EnumValue> enumDesignationForDesignOfHeatExchanger = new List<EnumValue>();
                enumDesignationForDesignOfHeatExchanger.Add(new EnumValue("1", "Zug-Kessel", ""));
                enumDesignationForDesignOfHeatExchanger.Add(new EnumValue("2", "Zug-Kessel Flammumkehr", ""));
                enumDesignationForDesignOfHeatExchanger.Add(new EnumValue("3", "Zug-Kessel", ""));
                enumDesignationForDesignOfHeatExchanger.Add(new EnumValue("999", "Sonstige", ""));

                Properties.Add(new Property(16, "CodeNumberForDesignOfHeatExchanger", record.Fields[16], "", "N", "", enumDesignationForModelOfHeatExchanger.Select(x => x.Code).ToList()));
                switch (countryCode)
                {
                    case "DE":
                        Properties.Add(new Property(17, "DesignationForDesignOfHeatExchanger", record.Fields[17], "", "A", "", enumDesignationForDesignOfHeatExchanger.Select(x => x.ValueDe).ToList()));
                        break;
                    case "EN":
                        Properties.Add(new Property(17, "DesignationForDesignOfHeatExchanger", record.Fields[17], "", "A", "", enumDesignationForDesignOfHeatExchanger.Select(x => x.ValueEn).ToList()));
                        break;
                }

                Properties.Add(new Property(18, "StandbyLossBoilerAtAverageWatertemperature70Degree", record.Fields[18], "%", "N", "DIN V 4701-10, otherwise function in accordance EN 304, EN 297, EN 656", null));

                List<EnumValue> enumDesignationForAirInlet = new List<EnumValue>();
                enumDesignationForAirInlet.Add(new EnumValue("1", "raumluftunabhängig", ""));
                enumDesignationForAirInlet.Add(new EnumValue("2", "raumluftabhängig", ""));
                enumDesignationForAirInlet.Add(new EnumValue("3", "raumluftabhängig oder -unabhängig", ""));
                enumDesignationForAirInlet.Add(new EnumValue("999", "Sonstige", ""));

                Properties.Add(new Property(19, "CodeNumberForAirInlet", record.Fields[19], "", "N", "", null));

                switch (countryCode)
                {
                    case "DE":
                        Properties.Add(new Property(20, "DesignationForAirInlet", record.Fields[20], "", "A", "", enumDesignationForAirInlet.Select(x => x.ValueDe).ToList()));
                        break;
                    case "EN":
                        Properties.Add(new Property(20, "DesignationForAirInlet", record.Fields[20], "", "A", "", enumDesignationForAirInlet.Select(x => x.ValueEn).ToList()));
                        break;
                }

                Properties.Add(new Property(21, "HeatingCirculatingPumpIntegratedInDevice", record.Fields[21], "", "N", "0 = no, 1 = yes", null));
                Properties.Add(new Property(22, "HeatingCirculatingPumpRegulated", record.Fields[22], "", "N", "0 = no, 1 = yes", null));
                Properties.Add(new Property(23, "AirConnection", record.Fields[23], "mm", "N", "", null));
                Properties.Add(new Property(24, "ExhaustConnection", record.Fields[24], "mm", "N", "", null));
            }

        }
    }
}
