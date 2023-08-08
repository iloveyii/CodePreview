using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;


namespace Accounting.Client.Extensions
{
    public class JSFunctionHandler
    {
        private IJSObjectReference jsModule;
        private IJSRuntime _jsRuntime;

        public JSFunctionHandler(IJSRuntime jSRuntime)
        {
            _jsRuntime = jSRuntime;
        }

        public async Task OpenFileDialog()
        {
            try
            {
                jsModule = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "/js/upload.js");
                await jsModule.InvokeVoidAsync("OpenUploadDialog");

            } catch (Exception ex) { 
                var msg = ex.Message;
            }
        }
    }
}
