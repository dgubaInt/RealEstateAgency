const ul = document.querySelector("#ul-tag"),
    input_tag = document.querySelector("#input-tag"),
    edit_input_tag = document.querySelector("#edit-input-tag"),
    input_to_send = document.querySelector("#input-to-send");

let tags = [];

if (edit_input_tag) {
    tags = edit_input_tag.value.split(' ');
    console.log(tags);
    tags.forEach(function (item) {
        if (item != '' && item != ' ') {
            createTag();
        }
    });
}

function createTag() {
    ul.querySelectorAll("li").forEach(li => li.remove());
    tags.slice().reverse().forEach(tag => {
        let liTag = `<li>${tag} <i onclick="remove(this, '${tag}')"></i></li>`;
        ul.insertAdjacentHTML("afterbegin", liTag);
    });
}
function remove(element, tag) {
    debugger;
    let index = tags.indexOf(tag);
    tags = [...tags.slice(0, index), ...tags.slice(index + 1)];
    element.parentElement.remove();
    input_to_send.value = tags;
    input_to_send.value.trim();
}
function addTag(e) {
    if (e.code == "Space") {
        let tag = e.target.value.replace(/\s+/g, ' ');
        input_to_send.value = `${input_to_send.value} ${tag.trim()}`.trim();
        if (tag.length > 1 && !tags.includes(tag)) {
            tag.split(',').forEach(tag => {
                tags.push(tag);
                createTag();
            });
        }
        e.target.value = "";
    }
}

input_tag.addEventListener("keyup", addTag);