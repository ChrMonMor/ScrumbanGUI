<script>
    import Item from './Items.vue'

    export default {
        props: {
            ItemsId: {
                type: Number,
                required: true
            },
        },
        name: 'App',
        components: {
            Item: Item
        },
        data() {
            return {
                posts: null
            };
        },
        computed: {
            startUrl() {
                return "https://localhost:7026/Items/" + this.ItemsId;
            }
        },
        mounted() {
            axios.get(startUrl())
                .then(response => (this.posts = response.data))
        }
    }
</script>

<template>
    <p>{{posts.Name}}</p>
    <p>{{posts.Content}}</p>
    <button>+</button>
</template>

<style>

</style>