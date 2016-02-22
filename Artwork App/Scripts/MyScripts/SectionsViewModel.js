function SectionsViewModel() {
    var self = this;
    this.SectionID = 0;
    this.Section = "";
}
SectionsViewModel.prototype.DoesThisSectionExist = function (strSectionName) {
    var retVal = -1
    //debugger;
    try {
        if (strSectionName) {
            var data = JSON.stringify(strSectionName);
            $.ajax({
                type: "POST",
                url: "/Sections/DoesSectionExist",
                data: data,
                datatype: "json",
                async: false,
                success: function (data) {
                    //return an object that has the error message and whterher the validation failed or succeded
                    //debugger;
                    if (data.Success) {
                        artistCount = data.Data;
                        retVal = data.Data;
                    }
                    else {
                        //debugger;
                        throw (data.Data);
                        artistCount = data.Data;
                        retVal = data.Data;
                    }

                },
                error: function () {
                    //return an object that has the error message and whterher the validation falided or succeded
                    //debugger;
                    throw ('Error in validating if arist exists');
                    artistCount = -1;
                    retVal = -1;
                }
            });
        }
        else {
            throw 'Section name cannot be Empty or null.';
        }

    } catch (e) {
        alert(e);
    }

    return retVal;
}
SectionsViewModel.prototype.InsertSection=function (strSectionName)
{
   //debugger;
    var retVal = -1;
    try {
        if (strSectionName) {
            //debugger
            var doesitExist = this.DoesThisSectionExist(strSectionName);
            if (doesitExist<=0) {
                var data = JSON.stringify(strSectionName);
                $.ajax({
                    type: "POST",
                    url: "/Sections/InsertSection",
                    data: data,
                    datatype: "json",
                    async: false,
                    success: function (data) {
                        //return an object that has the error message and whterher the validation failed or succeded
                        //debugger;
                        if (data.Success) {
                            artistCount = data.Data;
                            retVal = data.Data;
                        }
                        else {
                            ////////////////////////////////////////////////debugger;
                            throw (data.Data);
                            artistCount = data.Data;
                            retVal = data.Data;
                        }

                    },
                    error: function () {
                        //return an object that has the error message and whterher the validation falided or succeded
                        ////////////////////////////////////////////////debugger;
                        throw ('Error in inserting Section exists');

                        retVal = -1;
                    }
                });
            }
            else {
                throw 'The Section "'+strSectionName+'" already exists';
            }
           
        }
        else {
            throw 'Section name cannot be empty'
        }

    } catch (e) {
        alert(e);
    }

    return retVal;
}
