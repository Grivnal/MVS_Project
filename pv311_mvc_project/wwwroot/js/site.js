// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function postRequest(url, data) {
    fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: data
        })
        .then(response => {
            if (response.ok) {
                window.location.reload();
            }
        })
        .catch(error => console.error(error));
}

function addToCart(productId) {
    postRequest("/Cart/AddToCart", JSON.stringify({ productId: productId }));
}

function removeFromCart(productId) {
    postRequest("/Cart/RemoveFromCart", JSON.stringify({ productId: productId }));
}

function increaseQuantity(productId, quantity) {
    postRequest("/Cart/IncreaseQuantity", JSON.stringify({ productId: productId, quantity: quantity }));
}

function decreaseQuantity(productId, quantity) {
    postRequest("/Cart/DecreaseQuantity", JSON.stringify({ productId: productId, quantity: quantity }));
}

function activatePromoCode(code) {
    console.log(code);

    postRequest("/Cart/ActivatePromoCode", JSON.stringify(code));
}