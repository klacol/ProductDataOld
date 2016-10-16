using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDI3805
{
    public class HeatGenerator : ProductElementData_700  //VDI 3805 Part 3
    {
        public bool IsValid = true;

        public enum ModelOfHeatExchangerEnum { StandardKessel, NiedertemperaturKessel, BrennwertKessel, StandardKombiKessel, NiedertemperaturKombiKessel };
        public ModelOfHeatExchangerEnum ModelOfHeatExchanger { get; set; }

        public string SortNumberDisplaySequence;
        public string ProductRange;
        public string ProductName;
        public string NominalPower;
        public string TotalNetMass;
        public string WaterContent;
        public string GasContent;
        public string ThermalLoadLevel3;
        public string ThermalLoadLevel2;
        public string ThermalLoadLevel1;
        public string LowerModulationLimit;
        public string CodeNumberDesignationForModelOfHeatExchanger;
        public string CodeNumberDesignationHeatExchanger;
        public string DesignationForModelOfHeatExchanger;
        public string CodeNumberForDesignOfHeatExchanger;
        public string DesignationForDesignOfHeatExchanger;
        public string StandbyLossBoilerAtAverageWatertemperature70Degree;
        public string CodeNumberForAirInlet;
        public string DesignationForAirInlet;
        public string HeatingCirculatingPumpIntegratedInDevice;
        public string HeatingCirculatingPumpRegulated;
        public string AirConnection;
        public string ExhaustConnection;


        public HeatGenerator() { }

        public void Register(RecordSet record)
        {

            // The following list is copied from VDI 3805 , Part 3, Heat Generators
            // Do not add new attributes to the list, please follow the standard
            // The Copyright is at VDI Verein Deutscher Ingeniere
            // Please purchase a copy of the VDI 3805 before using this code in your application
            SortNumberDisplaySequence = record.Fields[3];
            ProductRange = record.Fields[4];
            ProductName = record.Fields[5];
            NominalPower = record.Fields[6];
            TotalNetMass = record.Fields[7];
            WaterContent = record.Fields[8];
            GasContent = record.Fields[9];
            ThermalLoadLevel3 = record.Fields[10];
            ThermalLoadLevel2 = record.Fields[11];
            ThermalLoadLevel1 = record.Fields[12];
            LowerModulationLimit = record.Fields[13];
            CodeNumberDesignationForModelOfHeatExchanger = record.Fields[14];
            DesignationForModelOfHeatExchanger = record.Fields[15];
            CodeNumberForDesignOfHeatExchanger = record.Fields[16];
            DesignationForDesignOfHeatExchanger = record.Fields[17];
            StandbyLossBoilerAtAverageWatertemperature70Degree = record.Fields[18];
            CodeNumberForAirInlet = record.Fields[19];
            DesignationForAirInlet = record.Fields[20];
            HeatingCirculatingPumpIntegratedInDevice = record.Fields[21];
            HeatingCirculatingPumpRegulated = record.Fields[22];
            AirConnection = record.Fields[23];
            ExhaustConnection = record.Fields[24];



            //SortNumberDisplaySequence = new Property(3,record.Fields[3], "", "N", "", null);
            //ProductRange=new Property(4,record.Fields[4], "", "A", "", null);
            //ProductName=new Property(5,record.Fields[5], "", "A", "", null);
            //NominalPower=new Property(6,record.Fields[6], "kW", "N", "", null);
            //TotalNetMass=new Property(7,record.Fields[7], "kg", "N", "Of heat generator", null);
            //WaterContent=new Property(8,record.Fields[8], "liter", "N", "Of heat generator", null);
            //GasContent=new Property(9,record.Fields[9], "liter", "N", "", null);
            //ThermalLoadLevel3=new Property(10,record.Fields[10], "kW", "N", "", null);
            //ThermalLoadLevel2=new Property(11,record.Fields[11], "kW", "N", "", null);
            //ThermalLoadLevel1=new Property(12,record.Fields[12], "kW", "N", "Min. load", null);
            //LowerModulationLimit=new Property(13,record.Fields[13], "kW", "N", "", null);
            //CodeNumberDesignationForModelOfHeatExchanger = new Property(14, record.Fields[14], "", "N", "Code number", null);
            //DesignationForModelOfHeatExchanger = new Property(15,record.Fields[15], "", "A", "", null);
            //CodeNumberForDesignOfHeatExchanger = new Property(16,record.Fields[16], "", "N", "", null);
            //DesignationForDesignOfHeatExchanger = new Property(17,record.Fields[17], "", "A", "", null);
            //StandbyLossBoilerAtAverageWatertemperature70Degree = new Property(18,record.Fields[18], "%", "N", "DIN V 4701-10, otherwise function in accordance EN 304, EN 297, EN 656", null);
            //CodeNumberForAirInlet = new Property(19, record.Fields[19], "", "N", "", null);
            //DesignationForAirInlet = new Property(20, record.Fields[20], "", "A", "", null);
            //HeatingCirculatingPumpIntegratedInDevice= new Property(21, record.Fields[21], "", "N", "0 = no, 1 = yes", null);
            //HeatingCirculatingPumpRegulated = new Property(22, record.Fields[22], "", "N", "0 , 1 = yes", null);
            //AirConnection= new Property(23,record.Fields[23], "mm", "N", "", null);
            //ExhaustConnection = new Property(24,record.Fields[24], "mm", "N", "", null);


            //List<EnumValue> enumDesignationForModelOfHeatExchanger = new List<EnumValue>();
            //enumDesignationForModelOfHeatExchanger.Add(new EnumValue("1", "Standard-Kessel", ""));
            //enumDesignationForModelOfHeatExchanger.Add(new EnumValue("2", "Niedertemperatur-Kessel", ""));
            //enumDesignationForModelOfHeatExchanger.Add(new EnumValue("3", "Brennwert-Kessel", ""));
            //enumDesignationForModelOfHeatExchanger.Add(new EnumValue("4", "Standard-Kombi-Kessel", ""));
            //enumDesignationForModelOfHeatExchanger.Add(new EnumValue("5", "Niedertemperatur-Kombi-Kessel", ""));
            //enumDesignationForModelOfHeatExchanger.Add(new EnumValue("6", "Brennwert-Kombi-Kessel", ""));
            //enumDesignationForModelOfHeatExchanger.Add(new EnumValue("7", "Durchlauferhitzer für TW-Bereitung", ""));
            //enumDesignationForModelOfHeatExchanger.Add(new EnumValue("8", "Trinkwasser-Speicher", ""));
            //enumDesignationForModelOfHeatExchanger.Add(new EnumValue("9", "Niederdruck-Dampferzeuger(NDD)", ""));
            //enumDesignationForModelOfHeatExchanger.Add(new EnumValue("10", "Hochdruck-Dampferzeuger(HDD)", ""));
            //enumDesignationForModelOfHeatExchanger.Add(new EnumValue("11", "Hochdruck-Heißwasser - Kessel", ""));
            //enumDesignationForModelOfHeatExchanger.Add(new EnumValue("999", "Sonstige", ""));


            //List<EnumValue> enumDesignationForAirInlet = new List<EnumValue>();
            //enumDesignationForAirInlet.Add(new EnumValue("1", "raumluftunabhängig", ""));
            //enumDesignationForAirInlet.Add(new EnumValue("2", "raumluftabhängig", ""));
            //enumDesignationForAirInlet.Add(new EnumValue("3", "raumluftabhängig oder -unabhängig", ""));
            //enumDesignationForAirInlet.Add(new EnumValue("999", "Sonstige", ""));


            //List<EnumValue> enumDesignationForDesignOfHeatExchanger = new List<EnumValue>();
            //enumDesignationForDesignOfHeatExchanger.Add(new EnumValue("1", "Zug-Kessel", ""));
            //enumDesignationForDesignOfHeatExchanger.Add(new EnumValue("2", "Zug-Kessel Flammumkehr", ""));
            //enumDesignationForDesignOfHeatExchanger.Add(new EnumValue("3", "Zug-Kessel", ""));
            //enumDesignationForDesignOfHeatExchanger.Add(new EnumValue("999", "Sonstige", ""));
        }
    }
}
