Test is in GM_Aplicom
C# 
else if (req.CalledFile == "testBin")
js in 
...\GM_Aplicom\GM_Aplicom\ZipRoot\web.zip

--------

We can created light-weight WebBiser, that will exchange several simple datatypes in binary format.

--------
js support numbers only up to
js +/- 9007199254740991   (2^53 -1)
c# -9223372036854775808    (make a check while encoding from C# to js) Encoder.JsAdd Decoder JsGetLong
 
no other decimal datatypes... only "long" ()
supports double (also with restrictions) so only double
strings must be converted as int array
byte[] can be supplied
datetimes?
collections as arrays
bool
byte
sbyte
nullables: (long?, double?, byte[], string, object) 

NaN for js


------------
C# 
 
            double flv = 124.56;
            var fltBts = BitConverter.GetBytes(flv);
            //var uBts = BitConverter.ToUInt64(fltBts, 0);

			
			


