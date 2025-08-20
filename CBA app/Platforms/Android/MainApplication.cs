using Android.App;
using Android.Runtime;

namespace CBA_app;

//#if DEBUG                                   // connect to local service on the
//  // emulator's host for debugging,
//#else                                       // access via http://10.0.2.2
//[Application]                               
//#endif
[Application(UsesCleartextTraffic = true)]
public class MainApplication : MauiApplication
{

    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
		: base(handle, ownership)
	{
	}

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
