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
        },
        name: 'App',
        components: {
            Item: Item
        },
        data() {
            return {
                items: null,
                posMay : 1
            };
        },
        mounted:function() {
            this.refreshData();
        },
        methods:{
            itemURL(){
                if(this.NotNewColumn){
                    return "https://localhost:7026/Columns/Items/" + this.ColumnsId;
                }
                return null;
            },
            createNewColumn(){
                axios.get("https://localhost:7026/Columns/Create/" + this.projectID +"/"+this.colNums+"/"+"PlaceholderName"+"/"+"255")
                .then(response => (this.refreshData(),this.items = response.data))
            },
            refreshData(){
                axios.get(this.itemURL())
                .then(response => (this.items = response.data, this.posMay = response.data.length+1, this.refreshData()))
            }
        }
    }

</script>

<template>
    <div v-if=this.NotNewColumn class="column">
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
}
</style>