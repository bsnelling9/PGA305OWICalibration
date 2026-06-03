using PGA305OWICalibration.Instruments;
using PGA305OWICalibration.PGA305EVM;
using System.Diagnostics;

namespace PGA305OWICalibration
{
    public partial class Form2 : Form
    {
        private USB2AnyDevice _u2a = new USB2AnyDevice();
        private PGA305Owi _pga305OWI = null!;

        //I2C variables for the Digipot used for OWI signal conditioning on the EVM.
        private const ushort DIGIPOT_ADDR = 0x2D;
        private const byte DIGIPOT_REG = 0x00;
        private const byte DIGIPOT_VALUE = 0x19;

        public Form2()
        {
            InitializeComponent();
        }

        private void btnExitDebug_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void u2a_Click(object sender, EventArgs e)
        {
            try
            {
                _u2a.EnableDebugLogging();

                int numFound = _u2a.FindControllers();
                listBoxDebug.Items.Add($"USB2ANY devices found: {numFound}");

                if (numFound == 0)
                {
                    listBoxDebug.Items.Add("No USB2ANY detected.");
                    return;
                }

                string serial = _u2a.GetSerialNumber(0);
                listBoxDebug.Items.Add($"USB2ANY serial: {serial}");

                bool opened = _u2a.Open("");
                if (!opened)
                {
                    listBoxDebug.Items.Add("Failed to open USB2ANY.");
                    return;
                }

                _u2a.Power_WriteControl(Power_3V3.ON, Power_5V0.ON);

                listBoxDebug.Items.Add($"USB2ANY opened. Handle = {_u2a.GetHandle()}");
                _pga305OWI = new PGA305Owi(_u2a);
            }
            catch (Exception ex)
            {
                listBoxDebug.Items.Add($"Error: {ex.Message}");
            }
        }

        private void btnHandlePOT_Click(object sender, EventArgs e)
        {
            int i2cResult = _u2a.I2C_Control(0, 0, 1);
            Debug.WriteLine($"I2C_Control result: {i2cResult}");

            int writeResult = _u2a.I2C_RegisterWrite(DIGIPOT_ADDR, DIGIPOT_REG, DIGIPOT_VALUE);
            Debug.WriteLine($"DigiPot write result: {writeResult}");

            int result = _u2a.I2C_RegisterWrite(0x57, 0x00, 0x00);
            Debug.WriteLine($"TPL0102 Pot0 write result: {result}");

            result = _u2a.I2C_RegisterWrite(0x57, 0x01, 0x00);
            Debug.WriteLine($"TPL0102 Pot1 write result: {result}");
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            if (_pga305OWI == null)
            {
                listBoxDebug.Items.Add("Please click U2A first.");
                return;
            }

            bool initOk = _pga305OWI.Initialize();
            listBoxDebug.Items.Add($"PGA305 init: {(initOk ? "OK" : "FAILED")}");
            if (initOk)
                listBoxDebug.Items.Add("Ready — click Activate.");
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            _pga305OWI.Activate();
        }

        private void btnReadDevice_Click(object sender, EventArgs e)
        {
            string partNumber = _pga305OWI.ReadPartNumber();
            string serialNumber = _pga305OWI.ReadSerialNumber();
            string internalSerialNumber = _pga305OWI.ReadInternalSerialNumber();

            listBoxDebug.Items.Add($"Part number:   {partNumber}");
            listBoxDebug.Items.Add($"Serial number: {serialNumber}");
            listBoxDebug.Items.Add($"Internal serial number: {internalSerialNumber}");
        }

        private void btnDebugGPIO_Click(object sender, EventArgs e)
        {
            try
            {
                _u2a.GPIO_SetPort(7, 1);

                listBoxDebug.Items.Add("GPIO7 → HIGH. Check TP51 with multimeter...");
                _u2a.GPIO_WritePort(7, 1);
                Thread.Sleep(2000);

                listBoxDebug.Items.Add("GPIO7 → LOW. Check TP51 with multimeter...");
                _u2a.GPIO_WritePort(7, 0);
                Thread.Sleep(2000);

                listBoxDebug.Items.Add("GPIO7 debug complete.");
            }
            catch (Exception ex)
            {
                listBoxDebug.Items.Add($"GPIO debug error: {ex.Message}");
            }
        }

        private void btnTestGPIO11_Click(object sender, EventArgs e)
        {
            try
            {
                listBoxDebug.Items.Add("--- GPIO11 Force Test ---");

                if (_u2a.GetHandle() <= 0)
                {
                    listBoxDebug.Items.Add("Device not open — click U2A first");
                    return;
                }

                int r1 = _u2a.GPIO_SetPort(11, 2);
                int r2 = _u2a.GPIO_WritePort(11, 2);
                listBoxDebug.Items.Add($"GPIO11 HIGH — SetPort:{r1} WritePort:{r2} — measure TP60 now");
                Thread.Sleep(3000);

                int r3 = _u2a.GPIO_WritePort(11, 1);
                listBoxDebug.Items.Add($"GPIO11 LOW — WritePort:{r3} — measure TP60 now");
                Thread.Sleep(3000);

                int r4 = _u2a.GPIO_WritePort(11, 2);
                listBoxDebug.Items.Add($"GPIO11 HIGH again — WritePort:{r4}");
                Thread.Sleep(3000);

                listBoxDebug.Items.Add("--- Did TP60 follow GPIO11? ---");
            }
            catch (Exception ex)
            {
                listBoxDebug.Items.Add($"Error: {ex.Message}");
            }
        }

        private void btnManualHardwareTest_Click(object sender, EventArgs e)
        {
            try
            {
                listBoxDebug.Items.Add("--- Manual Hardware Test ---");

                if (_u2a.GetHandle() <= 0)
                {
                    listBoxDebug.Items.Add("Device not open — click U2A first");
                    return;
                }

                int r = _u2a.Power_WriteControl(Power_3V3.ON, Power_5V0.ON);
                listBoxDebug.Items.Add($"Power ON result: {r}");
                Thread.Sleep(50);

                int r1 = _u2a.GPIO_SetPort(11, 1);
                int r2 = _u2a.GPIO_WritePort(11, 1);
                listBoxDebug.Items.Add($"GPIO11 HIGH — SetPort:{r1} WritePort:{r2}");
                Thread.Sleep(10);

                int r3 = _u2a.DACs_Write(DACs_WhichDAC.DAC0, DACs_OperatingMode.Normal, 0);
                listBoxDebug.Items.Add($"DAC0 = 0 result: {r3}");
                Thread.Sleep(10);

                int r4 = _u2a.GPIO_SetPort(10, 1);
                int r5 = _u2a.GPIO_WritePort(10, 1);
                listBoxDebug.Items.Add($"GPIO10 HIGH — SetPort:{r4} WritePort:{r5} — measure TP20 now");
                Thread.Sleep(3000);

                int r6 = _u2a.GPIO_WritePort(10, 0);
                listBoxDebug.Items.Add($"GPIO10 LOW — WritePort:{r6} — measure TP20 now");
                Thread.Sleep(3000);

                listBoxDebug.Items.Add("--- Did TP20 change between HIGH and LOW? ---");

                int r7 = _u2a.GPIO_SetPort(7, 1);
                int r8 = _u2a.GPIO_WritePort(7, 1);
                listBoxDebug.Items.Add($"GPIO7 HIGH — SetPort:{r7} WritePort:{r8} — measure TP4");
                Thread.Sleep(3000);

                int r9 = _u2a.GPIO_WritePort(7, 0);
                listBoxDebug.Items.Add($"GPIO7 LOW — WritePort:{r9}");
                Thread.Sleep(3000);

                int r10 = _u2a.GPIO_WritePort(7, 1);
                listBoxDebug.Items.Add($"GPIO7 HIGH again — WritePort:{r10}");
                Thread.Sleep(3000);

                _u2a.GPIO_WritePort(7, 0);
                listBoxDebug.Items.Add("--- Test complete ---");
            }
            catch (Exception ex)
            {
                listBoxDebug.Items.Add($"Error: {ex.Message}");
            }
        }

    }
}
