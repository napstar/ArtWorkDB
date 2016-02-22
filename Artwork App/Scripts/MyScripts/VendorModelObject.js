function VendorViewModel() {
    var self = this;
    this.ArtistID = 0;
    this.ArtistName = "";
}

VendorViewModel.prototype.DoesThisVendorExist = function (vendorName) {
    var retVal = -1
    try {
        ////////debugger
        var vendorCount = -1;
        if (vendorName) {
            var data = JSON.stringify(vendorName);
            $.ajax({
                type: "POST",
                url: "/Vendors/DoesThisVendorExist",
                data: data,
                datatype: "json",
                async: false,
                success: function (data) {
                    //return an object that has the error message and whterher the validation failed or succeded
                    ////////debugger;
                    if (data.Success) {
                        vendorCount = data.Data;
                        retVal = data.Data;
                    }
                    else {
                         ////////debugger;
                        throw (data.ErrorMessage);
                        vendorCount = data.Data;
                        retVal = data.Data;
                    }

                },
                error: function () {
                    //return an object that has the error message and whterher the validation falided or succeded
                     ////////debugger;
                    throw ('Error in validating if vendor exists');
                    vendorCount = -1;
                    retVal = -1;
                }
            });
        }
        else {
            //invalid no name
            alert("Vendor Name cannot be empty");
            retVal = -1;
        }
    } catch (e) {
        alert(e);
    }
    return retVal;
}

VendorViewModel.prototype.InsertVendor = function (vendorName) {
    ////////debugger;
    var returnVendorObj = VendorViewModel()
    try {
        var vendorCount = -1;
        if (vendorName) {
            var data = JSON.stringify(vendorName);
            $.ajax({
                type: "POST",
                url: "/Vendors/InsertNewVendor",
                data: data,
                datatype: "json",
                async: false,
                success: function (data) {
                    //return an object that has the error message and whterher the validation failed or succeded
                    ////////////////////////////////////////////debugger;
                    if (data.Success) {
                        returnVendorObj = data.Data;
                    }
                    else {
                        ////////////////////////////////////////////debugger;
                        throw (data.Data);
                        returnVendorObj = data.Data;
                    }

                },
                error: function () {
                    //return an object that has the error message and whether the validation falided or succeded
                    ////////////////////////////////////////////debugger;
                    throw ('Error in validating if vendor exists');
                    vendorCount = -1;
                }
            });
        }
        else {
            //invalid no name
            alert("vendor name cannot be empty");
        }
    } catch (e) {
        alert(e);
    }

    return returnVendorObj;
}
