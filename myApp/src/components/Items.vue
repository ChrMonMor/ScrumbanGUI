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
        methods:{
            createNewCard(){
                axios.get("https://localhost:7026/Items/Create/" + this.ItemColumn +"/"+this.maxPosi+"/"+"PlaceholderName"+"/"+"PlaceholderContent")
            }
        }
    }
</script>

<template>
    <div v-if=this.NotNewItem class="card">
        <p>{{this.ItemName}}</p>
        <p>{{this.ItemContent}}</p>
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