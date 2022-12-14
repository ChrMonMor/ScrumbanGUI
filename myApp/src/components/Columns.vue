<script>
import Item from './Items.vue'
import axios from 'axios';

    export default {
        props: {
            projectID:{
                type: Number,
            },
            colNums:{
                type: Number,
            },
            ColumnsId: {
                type: Number,
            },
            NotNewColumn:{
                type: Boolean,
                required: true
            },
            ColumnName:{
                type : String,
            },
            ColPos: {
                type: Number,
            },
            Limit:{
                type: Number,
            }
        },
        name: 'App',
        components: {
            Item: Item
        },
        data() {
            return {
                items: null,
                posMay : 1,
                colNewName :"",
                showV : false,
            };
        },
        mounted:function() {
            this.refreshData();
        },
        methods:{
            itemURL(){
                if(this.NotNewColumn){
                    return variables.API_URL+"Columns/Items/" + this.ColumnsId;
                }
                return null;
            },
            createNewColumn(){
                axios.get(variables.API_URL+"Columns/Create/" + this.projectID +"/"+this.colNums+"/"+"PlaceholderName"+"/"+"255")
                .then(response => (this.refreshData(),this.items = response.data))
            },
            refreshData(){
                axios.get(this.itemURL())
                .then(response => (this.items = response.data, this.posMay = response.data.length+1, this.refreshData()))
            },
            deleteClick(id){
                axios.get(variables.API_URL+"Columns/delete/"+id);
            },
            updateClick(id){
                axios.get(variables.API_URL+"Columns/Update/"+id+"/"+this.ColPos+"/"+this.colNewName+"/"+this.Limit)
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
    <div v-if=this.NotNewColumn class="column">
        <button id="deleteThis" @click=deleteClick(this.ColumnsId)>
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-x" viewBox="0 0 16 16">
                <path d="M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708z"/>
            </svg>
        </button>
        <h1 @dblclick="updateOptionClick()">{{this.ColumnName}}</h1> <div v-show="showV"><input type="text" class="form-control" v-model="colNewName"/> <button @click="updateClick(this.ColumnsId)">Rename</button></div>
        <Item v-for="n in items" :ItemId=n.Item_Id :ItemColumn=n.Column_Id :ItemPosition=n.Position :ItemName=n.Name :ItemContent=n.Content :NotNewItem=true></Item>
        <item :maxPosi=this.posMay :ItemColumn=this.ColumnsId :NotNewItem=false ></item>
    </div>
    <div v-else class="column">
        <button @click=createNewColumn()>+ Add New Category</button>
    </div>
</template>

<style>
.column {
  float: left;
  width: v-bind(colsNum);
  word-wrap: break-word;
}
</style>