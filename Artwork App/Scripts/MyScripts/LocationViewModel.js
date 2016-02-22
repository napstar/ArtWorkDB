function LocationViewModel()
{
    var self = this;
    this.LocationID = 0;
    this.LocationName = "";
}

LocationViewModel.prototype.DoesThisLocationExist=function (locationName)
{
    var retVal = -1;

    try {
        if (locationName) {
            var data = JSON.stringify(locationName);
            $.ajax({
                type: "POST",
                url: "/CurrentLocation/DoesLocationExist",
                data: data,
                datatype: "json",
                async: false,
                success: function (data) {
                    //return an object that has the error message and whether the validation failed or succeded
                    //////////////////////////////////////////debugger;
                    if (data.Success) {
                        retVal = data.Data;
                    }
                    else {
                        //////////////////////////////////////////debugger;
                        
                        retVal = data.Data;
                        throw (data.ErrorMessage);
                    }

                },
                error: function () {
                    //return an object that has the error message and whether the validation failed or succeded
                    //////////////////////////////////////////debugger;
                    throw ('Error in Inserting new Location ');
                    retVal = -1;
                }
            });
        }
        else {
            throw new Error('Location name cannot be empty');
        }

    } catch (e) {
        alert(e);
    }

    return retVal;
}

LocationViewModel.prototype.InsertNewLocation = function (locationName) {
    var returnLocationObj = new LocationViewModel();

    try {
        if (locationName) {

            var data = JSON.stringify(locationName);
            $.ajax({
                type: "POST",
                url: "/CurrentLocation/InsertNewLocation",
                data: data,
                datatype: "json",
                async: false,
                success: function (data) {
                    //return an object that has the error message and whether the validation failed or succeded
                    //////////////////////////////////////////debugger;
                    if (data.Success) {
                        returnLocationObj = data.Data;
                    }
                    else {
                        //////////////////////////////////////////debugger;
                        throw (data.ErrorMessage);
                        returnLocationObj = data.Data;
                    }

                },
                error: function () {
                    //return an object that has the error message and whether the validation failed or succeded
                    //////////////////////////////////////////debugger;
                    throw ('Error in Inserting new Location ');
                    artistCount = -1;
                }
            });
        }
        else {
            alert("Location name cannot be empty");
        }

    } catch (e) {
        alert(e);
    }


    return returnLocationObj;
}