function CountryViewModel() {
    var self = this;
    this.Country = 0;
    this.CountryName = "";
}
CountryViewModel.prototype.DoesThisCountryExist = function (countryName) {
    var retVal = -1
    try {
        var countryCount = -1;
        if (countryName) {
            var data = JSON.stringify(countryName);
            $.ajax({
                type: "POST",
                url: "/Country/DoesThisCountryExist",
                data: data,
                datatype: "json",
                async: false,
                success: function (data) {
                    //return an object that has the error message and whterher the validation failed or succeded
                    ////////////////////////////////////////debugger;
                    if (data.Success) {
                        countryCount = data.Data;
                        retVal = data.Data;
                    }
                    else {
                        ////////////////////////////////////////debugger;
                        throw (data.Data);
                        countryCount = data.Data;
                        retVal = data.Data;
                    }

                },
                error: function () {
                    //return an object that has the error message and whterher the validation falided or succeded
                    ////////////////////////////////////////debugger;
                    throw ('Error in validating if countryName exists');
                    countryCount = -1;
                    retVal = -1;
                }
            });
        }
        else {
            //invalid no name
            alert("country  name cannot be empty");
            retVal = -1;
        }
    } catch (e) {
        alert(e);
    }
    return retVal;
}

CountryViewModel.prototype.InsertCountry = function (countryName) {
    ////debugger;
    var returnCountryObj = CountryViewModel()
    try {
        var artistCount = -1;
        if (countryName) {
            var data = JSON.stringify(countryName);
            $.ajax({
                type: "POST",
                url: "/Country/InsertNewCountry",
                data: data,
                datatype: "json",
                async: false,
                success: function (data) {
                    //return an object that has the error message and whterher the validation failed or succeded
                    //////////////////////////////////////////debugger;
                    if (data.Success) {
                        returnCountryObj = data.Data;
                    }
                    else {
                        ////////////////////////////////////////debugger;
                        throw (data.Data);
                        returnCountryObj = data.Data;
                    }

                },
                error: function () {
                    //return an object that has the error message and whterher the validation falided or succeded
                    ////////////////////////////////////////debugger;
                    throw ('Error in validating if country exists');
                    artistCount = -1;
                }
            });
        }
        else {
            //invalid no name
            alert("Country name cannot be empty");
        }
    } catch (e) {
        alert(e);
    }

    return returnCountryObj;
}