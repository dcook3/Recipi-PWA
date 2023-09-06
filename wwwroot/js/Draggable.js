
const sleep = ms => new Promise(r => setTimeout(r, ms));

window.InitDraggable = async (id) => {
    var draggable = document.getElementById(id);
    var interaction = document.getElementById("interaction");
    var interactionBG = document.getElementById("interactionBG");
    draggable.parentElement.style.top = draggable.parentElement.parentElement.offsetHeight + "px";

    //wait for navbar to load
    await sleep(200)

    var draggableHeight = draggable.offsetHeight;
    var ogTop = draggable.parentElement.parentElement.offsetHeight - draggableHeight
    draggable.parentElement.style.top = ogTop + "px";
    interaction.style.top = "-" + interaction.offsetHeight + "px";
    interactionBG.style.width = interaction.offsetWidth + "px";
    interactionBG.style.height = interaction.offsetHeight + draggableHeight + "px";
    var handleMouseDown = (e) => {

        var handleMouseMove = (e) => {
            var mousePx = e.pageY - (draggableHeight / 2)
            if (mousePx < ogTop) {
                if (mousePx > (ogTop - draggableHeight) - interaction.offsetHeight) {
                    interaction.style.top = "-" + interaction.offsetHeight - (mousePx - ogTop) + "px";
                }
                else {
                    interaction.style.top = null;
                }
                draggable.parentElement.style.top = mousePx + "px";
            }
        }

        window.addEventListener("mousemove", handleMouseMove)

        window.addEventListener("mouseup", (e) => {

            window.removeEventListener("mousemove", handleMouseMove)
            if (parseInt(draggable.parentElement.style.top, 10) > draggable.parentElement.parentElement.offsetHeight / 2) {
                draggable.parentElement.style.top = ogTop + "px";
                interaction.style.top = "-" + interaction.offsetHeight + "px";
            }
            else {
                draggable.parentElement.style.top = "0px";
                interaction.style.top = null;
            }
        })
    }

    var handleTouch = (e) => {
        var handleMouseMove = (e) => {
            var mousePx = e.touches[0].pageY - (draggableHeight / 2)
            if (mousePx < ogTop) {
                if (mousePx > (ogTop - draggableHeight) - interaction.offsetHeight) {
                    interaction.style.top = "-" + interaction.offsetHeight - (mousePx - ogTop) + "px";
                }
                else {
                    interaction.style.top = null;
                }
                draggable.parentElement.style.top = mousePx + "px";
            }
        }

        window.addEventListener("touchmove", handleMouseMove)

        window.addEventListener("touchend", (e) => {

            window.removeEventListener("touchmove", handleMouseMove)
            if (parseInt(draggable.parentElement.style.top, 10) > draggable.parentElement.parentElement.offsetHeight / 2) {
                draggable.parentElement.style.top = ogTop + "px";
                interaction.style.top = "-" + interaction.offsetHeight + "px";
            }
            else {
                draggable.parentElement.style.top = "0px";
                interaction.style.top = null;
            }
        })
    }

    draggable.addEventListener("mousedown", handleMouseDown)
    draggable.addEventListener("touchstart", handleTouch)

}

