var currentList  = {};

function createShoppingList() {
    currentList.name = $("#shoppingListName").val();
    //initialize array of current shopping list objects
    currentList.items = new Array();

    //web service call
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "api/ShoppingList/",
        data: currentList,
        success: function (result) {
            showShoppingList();
        },
        error: function () {
            console.log("Something very bad happened");
        }
    });
}

function drawItems() {
    var $list = $("#shoppingListItems").empty();

    for (var i = 0; i < currentList.items.length; i++) {
        var currentItem = currentList.items[i];
        var $li = $("<li>").html(currentItem.name)
            .attr("id", "item_" + i);
        var $deleteBtn = $("<button onclick='deleteItem(" + currentItem.id + ")'>D</button>").appendTo($li);
        var $checkBtn = $("<button onclick='checkItem(" + currentItem.id + ")'>C</button>").appendTo($li);

        if (currentItem.checked) {
            $li.addClass("checked");
        }

        $li.appendTo($list);
    }
}

function deleteItem(itemId) {
    //currentList.items.splice(index, 1);
    //drawItems();

    $.ajax({
        type: "DELETE",
        dataType: "json",
        url: "api/Item/" + itemId,
        success: function (result) {
            currentList = result;
            drawItems();
        },
        error: function () {
            console.log("Something very bad happened");
        }
    });
}

function checkItem(itemId) {
    var changedItem = {};

    for (var i = 0; i < currentList.items.length; i++) {
        if (currentList.items[i].id == itemId) {
            changedItem = currentList.items[i];
        }
    }

    changedItem.checked = !changedItem.checked;

    $.ajax({
        type: "PUT",
        dataType: "json",
        url: "api/Item/" + itemId,
        data: changedItem,
        success: function (result) {
            currentList = result;
            drawItems();
        },
        error: function () {
            console.log("Something very bad happened");
        }
    });
    /*
    if ($("#item_" + index).hasClass("checked")) {
        $("#item_" + index).removeClass("checked");
    } else {
        $("#item_" + index).addClass("checked");
    }
    */
}

function getShoppingListById(id) {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "api/ShoppingList/" + id,
        success: function (result) {
            currentList = result;         
            showShoppingList();
            drawItems();
        },
        error: function () {
            console.log("Something very bad happened");
        }
    });

    /*
    console.info(id);

    currentList.name = "Mock Shopping List";
    currentList.items = [
        { name: "milk" },
        { name: "salad" },
        { name: "ice cream" }
    ];

    showShoppingList();
    drawItems();
    */
}

function addItem() {
    var newItem = {};
    newItem.name = $("#newItemName").val();
    newItem.shoppingListId = currentList.id;

    //currentList.items.push(newItem);
    //console.info(currentList);

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "api/Item/",
        data: newItem,
        success: function (result) {
            currentList = result;
            drawItems();
            $("#newItemName").val(""); //set value to empty string
        },
        error: function () {
            console.log("Something very bad happened");
        }
    });
}

function showShoppingList() {
    $("#shoppingListTitle").html(currentList.name);
    $("#shoppingListItems").empty();

    $("#createListDiv").hide();
    $("#shoppingListDiv").show();

    $("#newItemName").focus();
    $("#newItemName").keyup(function (event) { //listen for enter key
        if (event.keyCode == 13) {
            addItem();
        }
    });
}

$(document).ready(function () {
    console.info("ready");
    $("#shoppingListName").focus();

    $("#shoppingListName").keyup(function (event) { //listen for enter key
        if (event.keyCode == 13) {
            createShoppingList();
        }
    });

    //get current url (with id, if exists)
    var pageUrl = window.location.href;
    var idIndex = pageUrl.indexOf("?id=");
    if (idIndex != -1) {
        getShoppingListById(pageUrl.substring(idIndex + 4)); //4, because string ?id=   then it's the id
    }
});