<script>
import axios from 'axios';


    export default {        
        props: {
            ItemId: {
                type: Number,
            },
            ItemColumn:{
                type : Number,
            },
            ItemPosition: {
                type : Number,
            },
            ItemName: {
                type : String,
            },
            ItemContent: {
                type : String,
            },
            NotNewItem: {
                type: Boolean,
                required: true
            },
            maxPosi: {
                type: Number,
            }
        },
        data(){
            return{
                showV: false,
                itemNewName: "",
                itemNewContent: "",
                card:{
                    ItemId: 1,
                    ItemColumn: 1,
                    ItemPosition: 1,
                    ItemName: "",
                    ItemContent: "",
                    NotNewItem: true,
                    maxPosi: 1
                }
            }
        },
        methods:{
            createNewCard(){
                axios.get(variables.API_URL+"Items/Create/" + this.ItemColumn +"/"+this.maxPosi+"/"+"PlaceholderName"+"/"+"PlaceholderContent")
            },
            deleteClick(id){
      if(!confirm("Are you sure you want to delete "+ this.ItemName +"?")){
        return;
      }
                axios.get(variables.API_URL+"Items/delete/"+id);
            },
            updateClick(id,cid){
                if(this.itemNewName == ("",null)){
                    this.itemNewName = this.ItemName
                }
                if(this.itemNewContent == ("",null)){
                    this.itemNewName = this.ItemContent
                }                               
                axios.get(variables.API_URL+"Items/Update/"+id+"/"+cid+"/"+this.ItemPosition+"/"+this.itemNewName+"/"+this.itemNewContent)
                .then((response) => {
                    this.posts = response.data;
                    this.itemNewName = "";
                    this.itemNewContent = "";
                    document.getElementById("showItemNameEdit"+id).style.display = "none";
                    document.getElementById("showItemContentEdit"+id).style.display = "none";
                    document.getElementById("showItemNameName"+id).style.display = "block";
                    document.getElementById("showItemContentName"+id).style.display = "block";
                })
            },
            updateOptionClick(word,id){
                if (word == 'name') {
                document.getElementById("showItemNameEdit"+id).style.display = "block";
                document.getElementById("showItemNameName"+id).style.display = "none";
                this.itemNewName = this.ItemName;
                this.itemNewContent = this.ItemContent;
                document.getElementById("showItemNameEdit"+id).focus();
                } else {
                document.getElementById("showItemContentEdit"+id).style.display = "block";
                document.getElementById("showItemContentName"+id).style.display = "none";
                this.itemNewName = this.ItemName;
                this.itemNewContent = this.ItemContent;
                document.getElementById("showItemContentEdit"+id).focus();
                }
            },
            drag(ev) {
                ev.dataTransfer.setData("text", ev.target.id);
            },
            onDrop(ev) {
                const itemID = evt.dataTransfer.getData('itemID')      
                const item = this.items.find((item) => item.id == itemID)     
                item.list = list
            },
            startDrag(event, item) {
                event.dataTransfer.dropEffect = "move"
                evene.dataTransfer.effectAllowed = "move"
                event.dataTransfer.setData('itemID',item.id)
            },


        }
    }

</script>

<template>
    <div class="dropbasket" @drop="onDrop(event, this.ItemId, this.ItemColumn)"  @dragover.prevent  @dragenter.prevent>
        <div v-if=this.NotNewItem class="card blue" draggable="true" @dragstart="drag(event)" >
            <div class="lighter" @click="updateOptionClick('name',this.ItemId)">
                <h4 v-bind:id="'showItemNameName'+this.ItemId">{{this.ItemName}}</h4>
                <input v-bind:id="'showItemNameEdit'+this.ItemId" type="text" class="form-control" v-model="itemNewName" @focusout="updateClick(this.ItemId, this.ItemColumn)" @keyup.enter="updateClick(this.ItemId, this.ItemColumn)" style="display: none;"/> 
            </div>
            <div  class="lighter" @click="updateOptionClick('content', this.ItemId)">
                <p v-bind:id="'showItemContentName'+this.ItemId">{{this.ItemContent}}</p>
                <input v-bind:id="'showItemContentEdit'+this.ItemId" type="text" class="form-control" v-model="itemNewContent" @focusout="updateClick(this.ItemId, this.ItemColumn)" @keyup.enter="updateClick(this.ItemId, this.ItemColumn)" style="display: none;"/>
            </div>
            <pre class="close" id="deleteThis" >
                <svg @click=deleteClick(this.ItemId) xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-x" viewBox="0 0 16 16">
                    <path d="M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708z"/>
                </svg>
            </pre>
        </div>
        <div v-else class="card">
            <button @click=this.createNewCard()>+ Add new Card</button>
        </div>

    </div>
</template>

<style>
.dropbasket{
    min-height: 8px;
}
:root {
    --red: hsl(0, 78%, 62%);
    --cyan: hsl(180, 62%, 55%);
    --orange: hsl(34, 97%, 64%);
    --blue: hsl(212, 86%, 64%);
    --varyDarkBlue: hsl(234, 12%, 34%);
    --varyDark: hsl(234, 12%, 24%);
    --grayishBlue: hsl(229, 6%, 66%);
    --veryLightGray: hsl(0, 0%, 96%);
    --veryLight: hsl(0, 0%, 100%);
    --weight1: 200;
    --weight2: 400;
    --weight3: 600;
}
body {
    font-size: 15px;
    font-family: 'Poppins', sans-serif;
}
.card p {
    color: var(--grayishBlue);
    padding: 2px;
    text-align: center;
    word-wrap: break-word;
}

.card {
    border-radius: 8px;
    box-shadow: 0px 16px 20px -8px var(--grayishBlue);
    padding: 16px;
    margin: 8px;  
    background-color: var(--veryLightGray);
    width: inherit;
}
.lighter {
    background-color: var(--veryLight);
    border: 2px solid var(--veryLightGray);
    border-radius: 8px;
    margin-top: auto;
    margin-bottom: auto;
}
.close {
  position: absolute;
  padding: 4px;
  top: 0;
  right: 0;
}
.bi-x{
  cursor: pointer;
  color: var(--varyDarkBlue);
}

@media (max-width: 450px) {
    .card {
        height: 200px;
    }
}

@media (max-width: 950px) and (min-width: 450px) {
    .card {
        text-align: center;
        height: 180px;
    }
}
@media (min-width: 950px) {
    .box {
        width: 20%;
     
    }
    .header p {
        width: 30%;
    }
    
}
h4 {
    word-wrap: break-word;
    color: var(--varyDarkBlue);
    font-weight: var(--weight3);
    padding: 4px;
}
.cyan {
    border-top: 3px solid var(--cyan);
}
.red {
    border-top: 3px solid var(--red);
}
.blue {
    border-top: 3px solid var(--blue);
}
.orange {
    border-top: 3px solid var(--orange);
}
</style>