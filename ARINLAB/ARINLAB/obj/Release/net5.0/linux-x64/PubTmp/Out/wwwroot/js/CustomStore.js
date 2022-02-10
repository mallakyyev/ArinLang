var getDepartments = new DevExpress.data.AspNet.createStore({
    key: "id",
    loadUrl: "/api/DepartmentsAPI",
    onBeforeSend: function (method, ajaxOptions) {
        ajaxOptions.xhrFields = { withCredentials: true };
    }
});

var getMenus = new DevExpress.data.AspNet.createStore({
    key: "id",
    loadUrl: "/api/MenuAPI",
    onBeforeSend: function (method, ajaxOptions) {
        ajaxOptions.xhrFields = { withCredentials: true };
    }
});
var getIndustries = new DevExpress.data.AspNet.createStore({
    key: "id",
    loadUrl: "/api/IndustryAPI",
    onBeforeSend: function (method, ajaxOptions) {
        ajaxOptions.xhrFields = { withCredentials: true };
    }
});



