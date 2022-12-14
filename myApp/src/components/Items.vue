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
            }
        },
        methods:{
            createNewCard(){
                axios.get(variables.API_URL+"Items/Create/" + this.ItemColumn +"/"+this.maxPosi+"/"+"PlaceholderName"+"/"+"PlaceholderContent")
            },
            deleteClick(id){
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
                .then(response => (this.refreshData(),this.posts = response.data))
            },
            updateOptionClick(){
                if (this.showV) {
                    this.showV = false;
                } else {
                    this.showV = true;
                }
            },

        }
    }
</script>

<template>
    <div @dblclick="updateOptionClick()" v-if=this.NotNewItem class="card">
        <p >{{this.ItemName}}</p>
        <p >{{this.ItemContent}}</p>
        <div v-show="showV"><input type="text" class="form-control" v-model="itemNewName"/> <input type="text" class="form-control" v-model="itemNewContent"/><button @click="updateClick(this.ItemId, this.ItemColumn)">Rename</button></div>
        <button id="deleteThis" @click=deleteClick(this.ItemId)>
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-x" viewBox="0 0 16 16">
                <path d="M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708z"/>
            </svg>
        </button>
    </div>
    <div v-else class="card">
        <button @click=this.createNewCard()>+ Add new Card</button>
    </div>
</template>

<style>
.card{
    border: 1px solid black;
    padding: 2px;
    margin: 4px;
}
</style>