
const sleep = ms => new Promise(r => setTimeout(r, ms));

draggableInited = {}

window.InitDraggable = async (id) => {
    //if (!draggableInited[id]) {
        draggableInited[id] = true;
        var draggable = document.getElementById(id+"-draggable");
        var interaction = document.getElementById(id +"-interaction");
        var interactionBG = document.getElementById(id + "-interactionBG");
        

        var maxTop = 0;

        

        var draggableHeight
        var ogTop

        var init = () => {
            var postTop = document.getElementById(id + "-post-top");
            console.log(postTop)
            if (postTop) {
               
                maxTop = postTop.offsetHeight;
            }
            draggableHeight = draggable.offsetHeight;
            ogTop = draggable.parentElement.parentElement.offsetHeight - draggableHeight
            draggable.parentElement.style.top = ogTop + "px";
            interaction.style.top = "-" + interaction.offsetHeight + "px";
            interactionBG.style.width = interaction.offsetWidth + "px";
            interactionBG.style.height = interaction.offsetHeight + draggableHeight + "px";
        }

        init();

        new ResizeObserver(init).observe(draggable.parentElement.parentElement);

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
                    draggable.parentElement.style.top = maxTop + "px";
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
                    draggable.parentElement.style.top = maxTop + "px";
                    interaction.style.top = null;
                }
            })
        }

        draggable.addEventListener("mousedown", handleMouseDown)
        draggable.addEventListener("touchstart", handleTouch)
    //}
}

