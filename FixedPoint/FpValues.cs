//t4template 生成代码，切勿直接编辑！！！

namespace Unity.Mathematics.FixedPoint
{
    public partial struct Fp
    {
        /// Math.PI / 3
        public const long PI_OVER_3 = 0x10C152382;
        /// Math.PI / 4
        public const long PI_OVER_4 = 0xC90FDAA2;
        /// Math.E
        public const long E = 0x2B7E15162;
        /// 1/3
        public const long ONE_OVER_3 = 0x55555555;
        /// 0.25
        public const long ONE_OVER_4 = 0x40000000;
        /// 0.2
        public const long ONE_OVER_5 = 0x33333333;
        /// 1/6
        public const long ONE_OVER_6 = 0x2AAAAAAA;
        /// 1/12
        public const long ONE_OVER_12 = 0x15555555;
        /// 1/24
        public const long ONE_OVER_24 = 0xAAAAAAA;
        /// 1/720
        public const long ONE_OVER_720 = 0x5B05B0;
        /// 1e-7
        public const long ONE_E_MINUS_7 = 0x1AD;
        /// 1e-6
        public const long ONE_E_MINUS_6 = 0x10C6;
        /// 1e-5
        public const long ONE_E_MINUS_5 = 0xA7C5;
        /// 1e-4
        public const long ONE_E_MINUS_4 = 0x68DB8;
        /// 1e-3
        public const long ONE_E_MINUS_3 = 0x418937;
        /// 1e-2
        public const long ONE_E_MINUS_2 = 0x28F5C28;
        /// 5e-3
        public const long FIVE_E_MINUS_3 = 0x147AE14;
        /// 0.1
        public const long POINT_1 = 0x19999999;
        /// 0.2
        public const long POINT_2 = 0x33333333;
        /// 0.3
        public const long POINT_3 = 0x4CCCCCCC;
        /// 0.4
        public const long POINT_4 = 0x66666666;
        /// 0.5
        public const long POINT_5 = 0x80000000;
        /// 0.01
        public const long POINT_01 = 0x28F5C28;
        /// 0.02
        public const long POINT_02 = 0x51EB851;
        /// 0.03
        public const long POINT_03 = 0x7AE147A;
        /// 0.04
        public const long POINT_04 = 0xA3D70A3;
        /// 0.05
        public const long POINT_05 = 0xCCCCCCC;
        /// 0.001
        public const long POINT_001 = 0x418937;
        /// 0.002
        public const long POINT_002 = 0x83126E;
        /// 0.003
        public const long POINT_003 = 0xC49BA5;
        /// 0.004
        public const long POINT_004 = 0x10624DD;
        /// 0.005
        public const long POINT_005 = 0x147AE14;
        /// 0.0001
        public const long POINT_0001 = 0x68DB8;
        /// 0.0002
        public const long POINT_0002 = 0xD1B71;
        /// 0.0003
        public const long POINT_0003 = 0x13A92A;
        /// 0.0004
        public const long POINT_0004 = 0x1A36E2;
        /// 0.0005
        public const long POINT_0005 = 0x20C49B;
        /// 0.9999
        public const long POINT_9999 = 0xFFF97247;
        /// 0.9995
        public const long POINT_9995 = 0xFFDF3B64;
        /// 0.99999
        public const long POINT_99999 = 0xFFFF583A;
        /// Math.PI / 3
        public static Fp PiOver3 { get => new Fp(PI_OVER_3);}
        /// Math.PI / 4
        public static Fp PiOver4 { get => new Fp(PI_OVER_4);}
        /// Math.E
        public static Fp EFp { get => new Fp(E);}
        /// 1/3
        public static Fp OneOver3 { get => new Fp(ONE_OVER_3);}
        /// 0.25
        public static Fp OneOver4 { get => new Fp(ONE_OVER_4);}
        /// 0.2
        public static Fp OneOver5 { get => new Fp(ONE_OVER_5);}
        /// 1/6
        public static Fp OneOver6 { get => new Fp(ONE_OVER_6);}
        /// 1/12
        public static Fp OneOver12 { get => new Fp(ONE_OVER_12);}
        /// 1/24
        public static Fp OneOver24 { get => new Fp(ONE_OVER_24);}
        /// 1/720
        public static Fp OneOver720 { get => new Fp(ONE_OVER_720);}
        /// 1e-7
        public static Fp OneEMinus7 { get => new Fp(ONE_E_MINUS_7);}
        /// 1e-6
        public static Fp OneEMinus6 { get => new Fp(ONE_E_MINUS_6);}
        /// 1e-5
        public static Fp OneEMinus5 { get => new Fp(ONE_E_MINUS_5);}
        /// 1e-4
        public static Fp OneEMinus4 { get => new Fp(ONE_E_MINUS_4);}
        /// 1e-3
        public static Fp OneEMinus3 { get => new Fp(ONE_E_MINUS_3);}
        /// 1e-2
        public static Fp OneEMinus2 { get => new Fp(ONE_E_MINUS_2);}
        /// 5e-3
        public static Fp FiveEMinus3 { get => new Fp(FIVE_E_MINUS_3);}
        /// 0.1
        public static Fp Point1 { get => new Fp(POINT_1);}
        /// 0.2
        public static Fp Point2 { get => new Fp(POINT_2);}
        /// 0.3
        public static Fp Point3 { get => new Fp(POINT_3);}
        /// 0.4
        public static Fp Point4 { get => new Fp(POINT_4);}
        /// 0.5
        public static Fp Point5 { get => new Fp(POINT_5);}
        /// 0.01
        public static Fp Point01 { get => new Fp(POINT_01);}
        /// 0.02
        public static Fp Point02 { get => new Fp(POINT_02);}
        /// 0.03
        public static Fp Point03 { get => new Fp(POINT_03);}
        /// 0.04
        public static Fp Point04 { get => new Fp(POINT_04);}
        /// 0.05
        public static Fp Point05 { get => new Fp(POINT_05);}
        /// 0.001
        public static Fp Point001 { get => new Fp(POINT_001);}
        /// 0.002
        public static Fp Point002 { get => new Fp(POINT_002);}
        /// 0.003
        public static Fp Point003 { get => new Fp(POINT_003);}
        /// 0.004
        public static Fp Point004 { get => new Fp(POINT_004);}
        /// 0.005
        public static Fp Point005 { get => new Fp(POINT_005);}
        /// 0.0001
        public static Fp Point0001 { get => new Fp(POINT_0001);}
        /// 0.0002
        public static Fp Point0002 { get => new Fp(POINT_0002);}
        /// 0.0003
        public static Fp Point0003 { get => new Fp(POINT_0003);}
        /// 0.0004
        public static Fp Point0004 { get => new Fp(POINT_0004);}
        /// 0.0005
        public static Fp Point0005 { get => new Fp(POINT_0005);}
        /// 0.9999
        public static Fp Point9999 { get => new Fp(POINT_9999);}
        /// 0.9995
        public static Fp Point9995 { get => new Fp(POINT_9995);}
        /// 0.99999
        public static Fp Point99999 { get => new Fp(POINT_99999);}
    }
}
