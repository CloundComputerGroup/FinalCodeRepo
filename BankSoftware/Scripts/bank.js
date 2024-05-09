var app = angular.module('bankApp', ['ui.grid', 'ui.grid.treeView', 'ui.grid.pagination', 'ui.grid.autoResize', 'ui.grid.selection', 'ui.grid.exporter', 'ui.grid.cellNav', 'ui.grid.grouping', 'ui.grid.expandable']);

// Configure the $httpProvider by adding our date transformer
app.config(["$httpProvider", function ($httpProvider) {
    $httpProvider.defaults.transformResponse.push(function (responseData) {
        convertDateStringsToDates(responseData);
        return responseData;
    });
}]);

var jsonDateRegex = /^\/Date\((-?\d+)(?:-(\d+))?\)\/$/;

function convertDateStringsToDates(input) {
    // Ignore things that aren't objects.
    if (typeof input !== "object") return input;

    for (var key in input) {
        if (!input.hasOwnProperty(key)) continue;

        var value = input[key];
        var match;
        // Check for string properties which look like dates.
        if (typeof value === "string" && (match = value.match(jsonDateRegex))) {
            var milliseconds = parseInt(match[0].substr(6));
            if (!isNaN(milliseconds)) {
                input[key] = new Date(milliseconds);
            }
        } else if (typeof value === "object") {
            // Recurse into object
            convertDateStringsToDates(value);
        }
    }
}

app.filter('ampDateFormat', function ($filter) {
    return function (input) {
        if (input) {
            return $filter('date')(input, 'dd MMM yyyy');
        }
        return null;
    }
});
app.filter('ampDateFormatEmpty', function ($filter) {
    return function (input) {
        if (input) {
            if (input.getFullYear() < 1900)
                return "-";
            else {
                return $filter('date')(input, 'dd MMM yyyy HH:mm');
            }

        }
        return "-";
    }
});


app.filter('ampMonthFormat', function ($filter) {
    return function (input) {
        if (input) {
            return $filter('date')(input, 'MMM yyyy');
        }
        return null;
    }
});
app.filter('ampCurrencyFormat', function ($filter) {
    return function (input) {
        return $filter('currency')(input, '£', 0);
    }
});
// This filter will add % symbol to the value (i.e. 17% is 0.17).
app.filter('ampPercentageFormat', function ($filter) {
    return function (input) {
        return $filter('number')(input, 0) + '%';
    };
});

// This filter makes the assumption that the input will be in decimal form (i.e. 17% is 0.17).
app.filter('ampPercentageDecimalFormat', function ($filter) {
    return function (input) {
        return $filter('number')(input * 100, 0) + '%';
    };
});

// This filter will convert bool values (true/false) to YES/NO.
app.filter('ampBoolFormat', function ($filter) {
    return function (text, length, end) {
        if (text) {
            return 'YES';
        }
        return 'NO';
    }
});

// This filter will empty value to -
app.filter('ampEmptyFormat', function ($filter) {
    return function (text, length, end) {
        if (text == null || text == "") {
            return '-';
        }
        return text;
    }
});
// This filter will empty value to - and current filter
app.filter('ampCurrencyEmptyFormat', function ($filter) {
    return function (text) {
        if ((text == null && text != 0) || (text == "" && text != 0)) {
            return '-';
        }
        else {
            return $filter('currency')(text, '£', 2);
        }

    }
});

function dateStringFilter(searchTerm, cellValue) {
    return $.datepicker.formatDate("dd M yy", new Date(cellValue)).toLowerCase().indexOf(searchTerm.toLowerCase()) > -1;
}

function dateFormat(date, format) {
    return $.datepicker.formatDate(format, date);
}

apiGet = function ($http, uri, data, success, failure, always) {

    $http.get(uri, data)
        .then(function (result) {
            success(result);
            if (always != null)
                always();
        }, function (result) {
            if (failure != null) {
                failure(result);
            }

            if (always != null)
                always();
        });
}

apiPost = function ($http, uri, data, success, failure, always) {

    $http.post(uri, data)
        .then(function (result) {
            success(result);
            if (always != null)
                always();
        }, function (result) {
            if (failure != null) {
                failure(result);
            }
            else {
                var errorMessage = result.status + ':' + result.statusText;
                if (result.data != null && result.data.Message != null)
                    errorMessage += ' - ' + result.data.Message;
                self.modelErrors = [errorMessage];
                self.modelIsValid = false;
            }
            if (always != null)
                always();
        });
}

var postJSON = function (url, data, dataType, successcallback, errorcallback) {
    return $.ajax({
        type: "POST",
        url: url,
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        success: successcallback,
        error: errorcallback,
        dataType: dataType
    });
}

String.prototype.trimToLength = function (m) {
    return (this.length > m)
        ? jQuery.trim(this).substring(0, m) + "..."
        : this;
};

if (!String.format) {
    String.format = function (format) {
        var args = Array.prototype.slice.call(arguments, 1);
        return format.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != 'undefined'
                ? args[number]
                : match
                ;
        });
    };
}

getQueryVariable = function (variable) {
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        if (pair[0] == variable) { return pair[1]; }
    }
    return (false);
}

var appHostName = location.protocol + '//' + location.host;

getPropertyCardHtml = function (property, showViewButton, showStreetViewButton, showDistance, trimLength) {
    if (trimLength === undefined) {
        trimLength = 64;
    }
    var inner_html = "<div class='panel panel-default cp-property'>";
    inner_html += "  <div class='panel-body'>";
    inner_html += "      <div class='media'>";
    inner_html += "         <div class='media-left'>";
    if (property.ImageThumbnail == undefined) {
        inner_html += "             <img width='168px' height='170px' class='media-object propertyImage' src='" + appHostName + "/Images/no-image.png' />"
    } else {
        inner_html += "             <img width='168px' height='170px' class='media-object propertyImage' src='" + property.ImageThumbnail + "' alt='" + property.ImageTitle + "' />"
    }
    inner_html += "         </div>";

    var propertyDetails = (property.PropertyCode + " - " + property.PropertyAddress);
    var ownerDetails = (property.OwnerName);

    inner_html += "         <div class='media-body'>";
    inner_html += '             <h3 class="media-title" style=\"height: 30px;\" data-tooltip="' + propertyDetails + '">' + propertyDetails.trimToLength(trimLength) + '</h3>';
    inner_html += "             <dl class='cp-data-list'>"
    inner_html += "                 <dt>AM:</dt>";
    inner_html += "                 <dd>" + (property.AssetManager != null ? property.AssetManager : "-") + "</dd>";
    inner_html += "                 <dt>MS:</dt>";
    inner_html += "                 <dd>" + (property.ManagementSurveyor != null ? property.ManagementSurveyor : "-") + "</dd>";
    inner_html += "                 <dt>Units: " + property.UnitCount + "</dt>";
    inner_html += "                 <dd>Leases: " + property.LeaseCount + "</dd>";
    inner_html += "                 <dt>Rent (PA):</dt>";
    if (property.AnnualRent != null) {
        inner_html += "                 <dd>" + property.AnnualRent.toLocaleString('en-GB', { style: 'currency', currency: 'GBP' }).slice(0, -3) + "</dd>";
    }
    else {
        inner_html += "                 <dd> - </dd>";
    }

    if (showDistance) {
        inner_html += "             <dt>Distance:</dt>";
        inner_html += "             <dd>" + property.DistanceFromLocationKM + " KM</dd>";
    }
    inner_html += "             </dl>";
    if (showViewButton) {
        inner_html += '         <a class="btn btn-primary" onclick="javascript:loadDetailsFromCard(\'' + property.PropertyCode + '\');"><span class="glyphicon glyphicon-log-in" aria-hidden="true"></span>&nbsp;&nbsp;View</a>';
    }
    if (showViewButton) {
        //inner_html += '             <a id="btnSnapshot' + property.Id + '" data-iframe="true" data-src="' + appHostName + '/Property/Snapshot/' + property.PropertyCode + '" class="btn btn-primary" data-sub-html="<div class=\'custom-html\'><h4>Snapshot for ' + property.PropertyCode + ' - ' + property.PropertyAddress + '</h4></div>"><span class="glyphicon glyphicon-camera" aria-hidden="true"></span> Snapshot</a>';
        inner_html += '             <a id="btnSnapshot' + property.Id + '" data-iframe="true" data-src="' + appHostName + '/Property/Snapshot/' + property.PropertyCode + '" class="btn btn-primary"><span class="glyphicon glyphicon-camera" aria-hidden="true"></span> Snapshot</a>';
    }
    if (showStreetViewButton) {
        inner_html += '         <br/><a class="btn btn-primary" style="margin-top: 3px;" onclick="javascript:showPropertyPanorama(' + property.Latitude + ', ' + property.Longitude + ');"><span class="glyphicon glyphicon-road" aria-hidden="true"></span> Street View</a>';
    }
    inner_html += '         <input type="checkbox" aria-label="Favourited" id="fvt' + property.Id + '"' + (property.IsFavourite ? ' checked=""' : ' ') + ' />';
    inner_html += '         <label for="fvt' + property.Id + '" onclick="return toggleFavourite(event, this);" data-id="' + property.Id + '">Favourite</label>';
    inner_html += "			</div>";
    inner_html += "     </div>";
    inner_html += " </div>";
    inner_html += "</div>"

    //alert(inner_html);
    return inner_html;
}

getHtmlForDiv = function (url, divId, dto) {

    var ajaxLoading = "<img id='ajax-loader' src='" + appHostName + "/Images/ajax-loader.gif' align='left' style='height: 32px; width:32px; padding-right:5px;'>";

    $(divId).html("<p>" + ajaxLoading + "   Loading...</p>");

    $.ajax({
        url: url,
        type: "GET",
        dataType: "html",
        data: dto,
        cache: false,
        success: function (data) {
            //Fill div with results
            $(divId).html(data);
        },
        error: function (xhr, status, error) {
            alert(xhr.responseText);
        }
    });
}

getHtmlForDivAngular = function (url, divId, dto, $scope, $compile, onLoadFunction) {
    var ajaxLoading = "<img id='ajax-loader' src='" + appHostName + "/Images/ajax-loader.gif' align='left' style='height: 32px; width:32px; padding-right:5px;'>";

    $(divId).html("<p>" + ajaxLoading + "   Loading...</p>");

    $.ajax({
        url: url,
        type: "GET",
        dataType: "html",
        data: dto,
        cache: false,
        success: function (data) {
            //Fill div with results
            var html = $compile(data)($scope);
            $(divId).html(html);
            if (typeof onLoadFunction !== 'undefined') {
                onLoadFunction();
            }
        },
        error: function (xhr, status, error) {
            alert(xhr.responseText);
        }
    });
}

reportBadData = function (sectionString, userComments) {
    $.ajax({
        url: appHostName + "/Home/ReportBadData",
        type: "GET",
        data: { section: sectionString, comments: userComments },
        cache: false
    });
}

getHtmlForResultsCountDiv = function (divId, firstItem, lastItem, totalItems) {
    $(divId).html('(' + firstItem + ' - ' + lastItem + ' of ' + totalItems + ' results)');
}

configureTablePagination = function (paginationDivId, gridApi, $scope) {
    $(paginationDivId).bootpag({
        total: gridApi.pagination.getTotalPages(),
        page: gridApi.pagination.getPage(),
        maxVisible: 10,
        leaps: false,
        firstLastUse: true,
        prev: 'PREV',
        next: 'NEXT',
        first: 'FIRST',
        last: 'LAST',
        wrapClass: 'pagination',
        activeClass: 'active',
        disabledClass: 'disabled',
        nextClass: 'next',
        prevClass: 'prev',
        lastClass: 'last',
        firstClass: 'first'
    }).on("page", function (event, num) {
        gridApi.pagination.seek(num);

        $scope.$apply();
    });
}

updateTablePagination = function (paginationDivId, gridApi) {
    $(paginationDivId).bootpag({
        total: gridApi.pagination.getTotalPages(),
        page: gridApi.pagination.getPage()
    });
}

// Used by the OnClick method of the Favourite buttons
toggleFavourite = function (e, obj) {
    // Stop the event from bubbling up (in case it is nested inside a clickable div)
    var event = window.event || e;
    if (event.stopPropagation) {
        event.stopPropagation();
    }
    //IE8 and Lower
    else {
        event.cancelBubble = true;
    }

    var checkboxId = obj.getAttribute('for');
    var propertyId = obj.getAttribute('data-id');
    var url = appHostName + '/Home/ToggleFavouriteProperty/' + propertyId
    var checkbox = $('#' + checkboxId);
    $.getJSON(url)
        .done(function (data) {
            checkbox.prop('checked', data);
        });

    return false;
}

loadDetailsFromCard = function (propertyCode) {
    //var propertyCode = obj.getAttribute('data-code');
    var url = appHostName + '/Property/Details/' + propertyCode;
    document.location.href = url;
}

loadDetailsOnClick = function (property) {
    var propertyCode = property.PropertyCode;
    var url = appHostName + '/Property/Details/' + propertyCode;
    document.location.href = url;
}

loadDocumentOnClick = function (document) {
    var propertyId = document.Id;
    var urlPart1 = "http://filenet.grosvenor.com:9081/WorkplaceXT/getContent?id={";
    var urlPart2 = "}&objectType=Document&objectStoreName=Grosvenor";
    var url = urlPart1 + propertyId + urlPart2;

    window.open(url);
}

toggleGridBackground = function (obj) {
    var propertyCode = $.trim(obj.getAttribute('data-code'));
    var parent = $(obj).closest("div[ui-grid]").first();
    var row = $(parent).find("[data-code=" + propertyCode + "]");
    row.toggleClass('ui-grid-row-hover');
}

gridRowOver = function (obj) {
    toggleGridBackground(obj);
}

gridRowOut = function (obj) {
    toggleGridBackground(obj);
}

app.directive('starRating',
    function () {
        return {
            restrict: 'A',
            template: '<ul class="rating">'
                + '    <li ng-repeat="star in stars" ng-class="star" ng-click="toggle($index)">'
                + '\u2605'
                + '</li>'
                + '</ul>',
            scope: {
                ratingValue: '=',
                max: '=',
                onRatingSelected: '&'
            },
            link: function (scope, elem, attrs) {
                var updateStars = function () {
                    scope.stars = [];
                    scope.ratingValue = scope.ratingValue != undefined || scope.ratingValue != null ? scope.ratingValue : 0;
                    for (var i = 0; i < scope.max; i++) {
                        scope.stars.push({
                            filled: i < scope.ratingValue
                        });
                    }
                };

                scope.toggle = function (index) {
                    scope.ratingValue = index + 1;
                    scope.onRatingSelected({
                        rating: index + 1
                    });
                };

                scope.$watch('ratingValue',
                    function (oldVal, newVal) {
                        updateStars();
                    }
                );
            }
        };
    }
);