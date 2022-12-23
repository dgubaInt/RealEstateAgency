let files = [],
	dragArea = document.querySelector('.drag-area'),
	input = document.querySelector('.drag-area input'),
	button = document.querySelector('.card button'),
	select = document.querySelector('.drag-area .select'),
	container = document.querySelector('#image_container');

/** CLICK LISTENER */
if (select) {
	select.addEventListener('click', () => input.click());
}

/* INPUT CHANGE EVENT */
if (input) {
	input.addEventListener('change', () => {
		let file = input.files;
		// if user select no image
		if (file.length == 0) return;

		for (let i = 0; i < file.length; i++) {
			if (file[i].type.split("/")[0] != 'image') continue;
			if (!files.some(e => e.name == file[i].name)) files.push(file[i])
		}

		const dt = new DataTransfer()
		for (let i = 0; i < files.length; i++) {
			dt.items.add(files[i])
		}
		input.files = dt.files

		showImages();
	});
}


/** SHOW IMAGES */
function showImages() {
	container.innerHTML = files.reduce((prev, curr, index) => {
		return `${prev}
		    <div class="image" draggable="true">
			    <span onclick="delImage(${index})"></span>
			    <img draggable="false" src="${URL.createObjectURL(curr)}" />
			</div>`
	}, '');
}

/* DELETE IMAGE */
function delImage(index) {
	removeFileFromFileList(index);
	files.splice(index, 1);
	showImages();
}

$('#existingImages span').on("click", function () {
	$(this).closest("div").remove();
	$(this).closest("input").remove();
});

function removeFileFromFileList(index) {
	const dt = new DataTransfer()
	const { files } = input

	for (let i = 0; i < files.length; i++) {
		const file = files[i]
		if (index !== i)
			dt.items.add(file)
	}

	input.files = dt.files // Assign the updates list
}

if (dragArea) {
	/* DRAG & DROP */
	dragArea.addEventListener('dragover', e => {
		e.preventDefault()
		dragArea.classList.add('dragover')
	})

	/* DRAG LEAVE */
	dragArea.addEventListener('dragleave', e => {
		e.preventDefault()
		dragArea.classList.remove('dragover')
	});

	/* DROP EVENT */
	dragArea.addEventListener('drop', e => {
		e.preventDefault()
		dragArea.classList.remove('dragover');

		let file = e.dataTransfer.files;
		for (let i = 0; i < file.length; i++) {
			/** Check selected file is image */
			if (file[i].type.split("/")[0] != 'image') continue;

			if (!files.some(e => e.name == file[i].name)) files.push(file[i])
		}
		showImages();
	});
}
