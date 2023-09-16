
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
        new ResizeObserver(() => {
            if (parseInt(draggable.parentElement.style.top, 10) > draggable.parentElement.parentElement.offsetHeight / 2) {
                interaction.style.top = "-" + interaction.offsetHeight + "px";
            }
            else {
                interaction.style.top = null;
            }
            interactionBG.style.width = interaction.offsetWidth + "px";
            interactionBG.style.height = interaction.offsetHeight + draggableHeight + "px";
        }).observe(interaction);
        var handleMouseDown = (e) => {

            var handleMouseMove = (e) => {
                e.preventDefault();
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

var posts;
var currentIndex = 0; 

var initSinglePostSwipe = async (post, obj) => {
    var wrapper = post.parentElement;
    var video = post.querySelector("video");
    console.log(video);
    if (video) {
        var handleMouseDown = async (e) => {
            if (currentIndex > 0) {
                e.preventDefault();
            }
            console.log("hello")
            if (e.pageY) {

                var ogMousePX = e.pageY;
            }
            else {
                var ogMousePX = e.touches[0].pageY;
            }
            var lastMousePos = ogMousePX
            wrapper.style.transitionDuration = "0s";
            var setPostPos = async (direction = "stay") => {

                switch (direction) {
                    case "up":
                        if (currentIndex > 1) {
                            wrapper.prepend(wrapper.lastElementChild);
                            wrapper.style.transitionDuration = "0ms";
                            wrapper.style.top = (parseInt(wrapper.style.top, 10) - post.offsetHeight) + "px";
                            currentIndex = await obj.invokeMethodAsync("NextPost", "up", parseInt(wrapper.firstElementChild.id, 10))
                            wrapper.lastElementChild.querySelector("video").pause();
                            wrapper.children[1].querySelector("video").play();
                        }
                        else if (currentIndex == 1) {
                            currentIndex = await obj.invokeMethodAsync("NextPost", "up", parseInt(wrapper.firstElementChild.id, 10));
                            wrapper.firstElementChild.querySelector("video").play();
                            wrapper.children[1].querySelector("video").pause();
                        }
                        break;
                    case "down":

                        if (currentIndex == 0) {
                            currentIndex = await obj.invokeMethodAsync("NextPost", "down", parseInt(wrapper.lastElementChild.id, 10))
                            wrapper.firstElementChild.querySelector("video").pause();
                            wrapper.children[1].querySelector("video").play();
                        }

                        else {


                            wrapper.appendChild(wrapper.firstElementChild);
                            wrapper.style.top = (parseInt(wrapper.style.top, 10) + post.offsetHeight) + "px";
                            wrapper.firstElementChild.querySelector("video").pause();
                            wrapper.children[1].querySelector("video").play();
                            currentIndex = await obj.invokeMethodAsync("NextPost", "down", parseInt(wrapper.lastElementChild.id, 10))
                        }


                        break;
                }
                console.log(currentIndex)
                if (currentIndex == 0) {
                    await sleep(1);
                    wrapper.style.transitionDuration = "500ms";
                    wrapper.style.top = "0px";
                    await sleep(500);
                    wrapper.style.transitionDuration = "0ms";
                }
                else {
                    await sleep(1);
                    wrapper.style.transitionDuration = "500ms";
                    wrapper.style.top = -(post.offsetHeight) + "px";
                    await sleep(500);
                    wrapper.style.transitionDuration = "0ms";
                }
            }

            var handleMouseMove = (e) => {
                if (currentIndex > 0) {
                    e.preventDefault();
                }
                if (e.pageY) {

                    var py = e.pageY;
                }
                else {
                    var py = e.touches[0].pageY;
                }
                var mouseDiff = py - lastMousePos;
                lastMousePos = py;

                if (Math.abs(py - ogMousePX) < 165) {
                    wrapper.style.top = parseInt(wrapper.style.top, 10) + mouseDiff + "px";
                }
                else {
                    if (py < ogMousePX) {
                        setPostPos("down");
                    }
                    else {
                        setPostPos("up");
                    }
                    video.removeEventListener("mousemove", handleMouseMove)
                    video.removeEventListener("touchmove", handleMouseMove)
                    window.removeEventListener("mouseup", (e) => {
                        if (currentIndex > 0) {
                            e.preventDefault();
                        }
                        video.removeEventListener("mousemove", handleMouseMove)
                        video.removeEventListener("touchmove", handleMouseMove)
                        setPostPos();
                    })
                    window.removeEventListener("touchend", (e) => {
                        if (currentIndex > 0) {
                            e.preventDefault();
                        }
                        video.removeEventListener("mousemove", handleMouseMove)
                        video.removeEventListener("touchmove", handleMouseMove)
                        setPostPos();
                    })
                }
            }

            video.addEventListener("mousemove", handleMouseMove)
            video.addEventListener("touchmove", handleMouseMove)
            window.addEventListener("mouseup", (e) => {
                if (currentIndex > 0) {
                    e.preventDefault();
                }
                video.removeEventListener("mousemove", handleMouseMove)
                video.removeEventListener("touchmove", handleMouseMove)
                setPostPos();
            })
            window.addEventListener("touchend", (e) => {
                e.preventDefault();
                video.removeEventListener("mousemove", handleMouseMove)
                video.removeEventListener("touchmove", handleMouseMove)
                setPostPos();
            })
        }

        video.addEventListener("mousedown", handleMouseDown)
        video.addEventListener("touchstart", handleMouseDown)
    }
}

window.InitPostsSwipe = async (obj) => {
    posts = document.querySelectorAll(".post-wrapper");
    currentIndex = 0;
    posts.forEach((post) => initSinglePostSwipe(post, obj))
}

window.InitPostSwipe = async (id, obj) => {
    post = document.getElementById(id);
    initSinglePostSwipe(post, obj)
}