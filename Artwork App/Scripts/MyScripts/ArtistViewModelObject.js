function ArtistViewModel() {
    var self = this;
    this.ArtistID = 0;
    this.ArtistName = "";
}

ArtistViewModel.prototype.DoesThisArtistExist = function (artistName) {
    var retVal=-1
    try {
        var artistCount = -1;
        if (artistName) {
            var data = JSON.stringify(artistName);
            $.ajax({
                type: "POST",
                url: "/Artist/DoesThisArtistExist",
                data: data,
                datatype: "json",
                async: false,
                success: function (data) {
                    //return an object that has the error message and whterher the validation failed or succeded
                    ////////////////////////////////////////////debugger;
                    if (data.Success) {
                        artistCount = data.Data;
                        retVal = data.Data;
                    }
                    else {
                        ////////////////////////////////////////////debugger;
                        throw (data.Data);
                        artistCount = data.Data;
                        retVal = data.Data;
                    }

                },
                error: function () {
                    //return an object that has the error message and whterher the validation falided or succeded
                    ////////////////////////////////////////////debugger;
                    throw ('Error in validating if arist exists');
                    artistCount = -1;
                    retVal = -1;
                }
            });
        }
        else {
            //invalid no name
            alert("Artist name cannot be empty");
            retVal = -1;
        }
    } catch (e) {
        alert(e);
    }
    return retVal;
}

ArtistViewModel.prototype.InsertArtist = function (artistName) {
    ////////debugger;
   var returnArtistObj=  ArtistViewModel()
    try {
        var artistCount = -1;
        if (artistName) {
            var data = JSON.stringify(artistName);
            $.ajax({
                type: "POST",
                url: "/Artist/InsertNewRecordNO_EF",
                data: data,
                datatype: "json",
                async: false,
                success: function (data) {
                    //return an object that has the error message and whterher the validation failed or succeded
                    ////////////////////////////////////////////debugger;
                    if (data.Success) {
                        returnArtistObj = data.Data;
                    }
                    else {
                        ////////////////////////////////////////////debugger;
                        throw (data.Data);
                        returnArtistObj = data.Data;
                    }

                },
                error: function () {
                    //return an object that has the error message and whterher the validation falided or succeded
                    ////////////////////////////////////////////debugger;
                    throw ('Error in validating if arist exists');
                    artistCount = -1;
                }
            });
        }
        else {
            //invalid no name
            alert("Artist name cannot be empty");
        }
    } catch (e) {
        alert(e);
    }

    return returnArtistObj;
}