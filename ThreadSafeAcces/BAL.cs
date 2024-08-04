public class CallingApp
{
    public static async Task Run()
    {
        await Task.Run(async() => {
            List<string> X = null;
            while (true)
            {
                                           
                List<string> strings = await ThirdPartyApp.GetPropAsync();
                await Task.Delay(200);  
                //Let the ThirdParty List Load some values
                if(X != null && X.Count == strings.Count)   
                {
                    Console.WriteLine("Closing task. No new elements");
                    break;
                }
                X = new List<string>(strings);                
                foreach (string item in X)
                {
                    Console.WriteLine(item.ToString());                    
                }
            }
        });
    }
}